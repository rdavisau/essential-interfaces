using System;
using System.Collections.Generic;
using System.Linq;
using EssentialInterfaces.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using CSharpExtensions = Microsoft.CodeAnalysis.CSharpExtensions;

namespace EssentialInterfaces.Helpers
{
    public static class Ext
    {
        public static List<T> GetDeclarations<T>(this SyntaxTree tree, bool nest = false)
            where T : SyntaxNode
            => new DeclarationSyntaxWalker<T>
            {
                Descend = nest
            }
            .Visit(tree);

        public static List<T> GetDeclarations<T>(this SyntaxNode tree, bool nest = false)
            where T : SyntaxNode
            => new DeclarationSyntaxWalker<T>
                {
                    Descend = nest
                }
                .VisitNode(tree);

        public static bool Contains(this SyntaxTokenList list, SyntaxKind modifier)
            => list.Any(x => CSharpExtensions.IsKind((SyntaxToken) x, modifier));

        public static bool IsPublicSettable(this PropertyDeclarationSyntax p)
            => p.AccessorList?.Accessors.Any(x => x.IsKind(SyntaxKind.SetAccessorDeclaration)
                                                  && !x.Modifiers.Any(m => m.IsKind(SyntaxKind.PrivateKeyword))) ?? false;

        public static bool HasTypeConstraintClauses(this ApiMemberModel m)
            => !String.IsNullOrWhiteSpace(m.TypeConstraints);

        public static SyntaxToken GetIdentifier(this EventFieldDeclarationSyntax evt)
            => evt.Declaration.Variables[0].Identifier;
        
        public static string Indent(this string s)
            => String.Join(Environment.NewLine,
                s.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(str => $"\t{str}"));

        public static T Dump<T>(this T obj, string heading = null)
        {
            if (heading != null)
                Console.WriteLine(heading);

            Console.WriteLine(obj is string ? $"{obj}" : JsonConvert.SerializeObject(obj, Formatting.Indented));

            return obj;
        }
    }
}