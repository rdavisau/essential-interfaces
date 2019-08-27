using System;
using System.Collections.Generic;
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
        private readonly List<string> _masks = new[] { ".netstandard", ".shared" }.ToList();

        public List<ApiModel> Generate(GeneratorContext context)
            =>
                Directory
                    .GetFiles(context.XamarinEssentialsImplementationsPath, "*.cs", SearchOption.AllDirectories)
                    .Where(f => _masks.Any(f.Contains))
                    .GroupBy(Path.GetDirectoryName, File.ReadAllText)
                    .Select(a =>
                    {
                        var api = Path.GetFileName(a.Key);
                        var tree = CSharpSyntaxTree.ParseText(String.Join(Environment.NewLine, a));
                        var ns = $"{tree.GetDeclarations<NamespaceDeclarationSyntax>().FirstOrDefault()?.Name.ToString()}";

                        return new ApiModel
                        {
                            Namespace = ns,
                            Api = api,
                            Declarations =
                                tree.GetDeclarations<ClassDeclarationSyntax>()
                                    .Where(x => x.Identifier.Text == api)
                                    .SelectMany(GetQualifyingDeclarations)
                                    .Select(GetModelForDeclaration)
                                    .ToList()
                        };
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
                cls.GetDeclarations<EventDeclarationSyntax>()
                    .Where(m => m.Modifiers.Contains(SyntaxKind.StaticKeyword));

            return Enumerable.Concat<MemberDeclarationSyntax>(methods, properties).Concat(events);
        }

        public ApiMemberModel GetModelForDeclaration<T>(T syntaxNode)
            where T : SyntaxNode
        {
            switch (syntaxNode)
            {
                case MethodDeclarationSyntax m:
                    return new ApiMemberModel
                    {
                        Kind = MemberKind.Method,
                        Identifier = $"{m.Identifier}{m.GetRequiredGenericTypeArgumentsIfAny()}",
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
                    throw new NotImplementedException($"Don't know how to generate model from {typeof(T)}");
            }
        }
    }
}