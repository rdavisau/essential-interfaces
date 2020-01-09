using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using EssentialInterfaces.Helpers;
using EssentialInterfaces.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EssentialInterfaces.Tasks
{
    public class RoslynModelGenerator
    {
        private readonly List<string> _masks = new[] { ".netstandard.cs", ".shared.cs" }.ToList();

        public List<ApiModel> Generate(GeneratorContext context)
            =>
                Directory
                    .GetFiles(context.XamarinEssentialsImplementationsPath, "*.cs", SearchOption.AllDirectories)
                    .Where(f => _masks.Any(f.EndsWith))
                    .GroupBy(Path.GetDirectoryName, f => (f, contents: File.ReadAllText(f)))
                    .Select(a =>
                    {
                        var api = Path.GetFileName(a.Key);
                        var tree = CSharpSyntaxTree.ParseText(String.Join(Environment.NewLine, a.Select(f => f.contents)));
                        var ns = $"{tree.GetDeclarations<NamespaceDeclarationSyntax>().FirstOrDefault()?.Name.ToString()}";

                        var apiModel = new ApiModel
                        {
                            Namespace = ns,
                            Api = api,
                            Declarations =
                                tree.GetDeclarations<ClassDeclarationSyntax>()
                                    .Where(x => x.Identifier.Text == api)
                                    .SelectMany(GetQualifyingDeclarations)
                                    .Select(GetModelForDeclaration)
                                    .Where(m => m != null)
                                    .ToList(),
                            OtherTypes = 
                                tree.GetDeclarations<ClassDeclarationSyntax>(true)
                                    .Where(x => x.Identifier.Text != api)
                                    .Select(x => $"{x.Identifier.Text}")
                                    .ToList()
                        };

                        foreach (var declarationWithConstraint in apiModel.Declarations.Where(x => x.HasTypeConstraintClauses()))
                            QualifyNestedTypeConstraintIfNecessary(declarationWithConstraint, apiModel);

                        return apiModel;
                    })
                    .Where(a => a.Declarations.Any() || $"Ignoring {a.Api} which has no qualifying implementations".Dump() == null)
                    .ToList()
                    .Dump("Generated Models:");

        public IEnumerable<MemberDeclarationSyntax> GetQualifyingDeclarations(ClassDeclarationSyntax cls)
        {
            // we care about public static methods, properties and events	
            var methods =
                cls.GetDeclarations<MethodDeclarationSyntax>()
                    .Where(m => m.Modifiers.Contains(SyntaxKind.PublicKeyword)
                                && m.Modifiers.Contains(SyntaxKind.StaticKeyword))
                    .Select(m => m.WithModifiers(new SyntaxTokenList()));

            var properties =
                cls.GetDeclarations<PropertyDeclarationSyntax>()
                    .Where(m => m.Modifiers.Contains(SyntaxKind.PublicKeyword)
                                && m.Modifiers.Contains(SyntaxKind.StaticKeyword));

            var events =
                Enumerable.Concat<MemberDeclarationSyntax>
                    (
                        cls.GetDeclarations<EventDeclarationSyntax>().Where(m => m.Modifiers.Contains(SyntaxKind.StaticKeyword)),
                        cls.GetDeclarations<EventFieldDeclarationSyntax>()
                    );

            return Enumerable.Concat<MemberDeclarationSyntax>(methods, properties).Concat(events);
        }

        public ApiMemberModel GetModelForDeclaration<T>(T syntaxNode)
            where T : SyntaxNode
        {
            switch (syntaxNode)
            {
                case MethodDeclarationSyntax m when m.TypeParameterList?.Parameters.Any() ?? false:
                    return new ApiMemberModel
                    {
                        Kind = MemberKind.Method,
                        Identifier = $"{m.Identifier}",
                        TypeParameters = $"{m.TypeParameterList}",
                        TypeConstraints = $"{m.ConstraintClauses}",
                        ReturnType = $"{m.ReturnType}",
                        ArgsString = $"{m.ParameterList.ToFullString().Trim()}"
                    };
                
                case MethodDeclarationSyntax m:
                    return new ApiMemberModel
                    {
                        Kind = MemberKind.Method,
                        Identifier = $"{m.Identifier}",
                        TypeParameters = $"{m.TypeParameterList}",
                        ReturnType = $"{m.ReturnType}",
                        ArgsString = $"{m.ParameterList.ToFullString().Trim()}"
                    };

                case EventDeclarationSyntax evt:
                    return new ApiMemberModel
                    {
                        Kind = MemberKind.Event,
                        Identifier = $"{evt.Identifier}",
                        ReturnType = $"{evt.Type}"
                    };

                case EventFieldDeclarationSyntax evt 
                    when !$"{evt.GetIdentifier()}".EndsWith("Internal"):
                    return new ApiMemberModel
                    {
                        Kind = MemberKind.Event,
                        Identifier = $"{evt.GetIdentifier()}",
                        ReturnType = $"{evt.Declaration.Type}"
                    };

                // we don't want to exposed internal event handlers
                case EventFieldDeclarationSyntax evt
                    when $"{evt.GetIdentifier()}".EndsWith("Internal"):
                    return null;

                case PropertyDeclarationSyntax p
                    when p.AccessorList?.Accessors.Any(x => x.IsKind(SyntaxKind.SetAccessorDeclaration) && !x.Modifiers.Any(m => m.IsKind(SyntaxKind.PrivateKeyword))) ?? false:
                    return new ApiMemberModel
                    {
                        Kind = MemberKind.Property,
                        ReturnType = $"{p.Type}",
                        Identifier = $"{p.Identifier}"
                    };

                case PropertyDeclarationSyntax p:
                    return new ApiMemberModel
                    {
                        Kind = MemberKind.PropertyGetOnly,
                        ReturnType = $"{p.Type}",
                        Identifier = $"{p.Identifier}"
                    };

                default:
                    throw new NotImplementedException($"Don't know how to generate model from {syntaxNode.GetType()}");
            }
        }

        // some type constraints against member methods (so far, just permissions ones) refer to types nested within the api
        // this is difficult to detect using a non-semantic model, so compare all nested types with the type constraint to 
        // detect any matches.
        // since this could result in unexpected things in the future, let's throw for anything we don't expect
        private readonly string[] _expectedNestedTypeConstraintApis = { "Permissions" };
        private readonly string[] _ignoreNestedTypeConstraintApis = { };
        private void QualifyNestedTypeConstraintIfNecessary(ApiMemberModel declarationWithConstraint, ApiModel api)
        {
            var matchingInternalType =
                api.OtherTypes
                    .FirstOrDefault(x => declarationWithConstraint.TypeConstraints.Contains(x));

            if (matchingInternalType is null || _ignoreNestedTypeConstraintApis.Contains(api.Api))
                return;

            if (!_expectedNestedTypeConstraintApis.Contains(api.Api))
                throw new Exception(
                    $"Found a potentially nested type in member '{declarationWithConstraint.Identifier}', " +
                    $"but the containing api '{api.Api}' was not listed in {nameof(_expectedNestedTypeConstraintApis)} or {nameof(_ignoreNestedTypeConstraintApis)}");

            $"Replacing '{matchingInternalType}' found in type constraint for member '{api.Api}.{declarationWithConstraint.Identifier}' with fully qualified nested name."
                .Dump();

            declarationWithConstraint.TypeConstraints =
                declarationWithConstraint.TypeConstraints.Replace(matchingInternalType,
                    $"{api.Namespace}.{api.Api}.{matchingInternalType}");
        }
    }
}