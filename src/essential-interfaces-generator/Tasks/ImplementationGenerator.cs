using System;
using System.Collections.Generic;
using System.Linq;
using EssentialInterfaces.Helpers;
using EssentialInterfaces.Models;

namespace EssentialInterfaces.Tasks
{
    public class ImplementationGenerator
    {
        private const string InterfacesNamespace = "Xamarin.Essentials.Interfaces";
        private const string ImplementationNamespace = "Xamarin.Essentials.Implementation";
        private const string ImplementationClass = "EssentialsImplementation";

        private readonly List<string> _usings
            = new[] { "System", "System.Collections.Generic", "System.IO", "System.Threading", "System.Threading.Tasks", InterfacesNamespace }.ToList();
            
        public string Generate(GeneratorContext context, List<ApiModel> models)
        {
            var interfaces = GetInterfacesCode(models);
            var implementation = GetImplementationCode(models);
            var output =
                $"{String.Join(Environment.NewLine, _usings.Select(u => $"using {u};"))}{Environment.NewLine}{Environment.NewLine}" +
                $"namespace {InterfacesNamespace}{{{Environment.NewLine}{interfaces.Indent()}{Environment.NewLine}}}{Environment.NewLine}{Environment.NewLine}" +
                $"namespace {ImplementationNamespace}{{{Environment.NewLine}{implementation.Indent()}{Environment.NewLine}}}";

            return output;
        }

        public string GetInterfacesCode(List<ApiModel> models)
            => String.Join(Environment.NewLine,
                models.Select(m =>
                    $"public interface {m.Interface} {{ {Environment.NewLine}" +
                    String.Join(Environment.NewLine, m.Declarations.Select(GetInterfacePrototype)).Indent() +
                    Environment.NewLine +
                    $"}}"
                ));

        public string GetImplementationCode(List<ApiModel> models)
            => $"public class {ImplementationClass} : {String.Join(", ", models.Select(i => i.Interface))} {{ {Environment.NewLine}{Environment.NewLine}" +
               String.Join($"{Environment.NewLine}{Environment.NewLine}", models.SelectMany(x => x.Declarations, GetForwardedImplementation)).Indent() + Environment.NewLine +
               $"}}";


        public string GetInterfacePrototype(ApiMemberModel model)
        {
            switch (model.Kind)
            {
                case MemberKind.Method:
                    return $"{model.ReturnType} {model.Identifier}{model.ArgsString};";

                case MemberKind.Event:
                    return $"event {model.ReturnType} {model.Identifier};";

                case MemberKind.Property:
                    return $"{model.ReturnType} {model.Identifier} {{ get; set; }}";

                case MemberKind.PropertyGetOnly:
                    return $"{model.ReturnType} {model.Identifier} {{ get; }}";

                default:
                    throw new NotImplementedException($"Don't know how to generate prototype for {model.Kind}");
            }
        }

        private string GetForwardedImplementation(ApiModel m, ApiMemberModel d)
        {
            switch (d.Kind)
            {
                case MemberKind.Method:
                    return $"{d.ReturnType} {m.Interface}.{d.Identifier}{d.ArgsString}{Environment.NewLine}" +
                           $"\t => {m.Namespace}.{m.Api}.{d.Identifier}({d.ArgsArgs});";

                case MemberKind.Event:
                    return $"event {d.ReturnType} {m.Interface}.{d.Identifier}{Environment.NewLine}{{{Environment.NewLine}" +
                           $"\t add => {m.Namespace}.{m.Api}.{d.Identifier} += value; {Environment.NewLine}" +
                           $"\t remove => {m.Namespace}.{m.Api}.{d.Identifier} -= value; {Environment.NewLine} }}";

                case MemberKind.Property:
                    return $"{d.ReturnType} {m.Interface}.{d.Identifier}{Environment.NewLine}{{{Environment.NewLine}" +
                           $"\t get {{ return {m.Namespace}.{m.Api}.{d.Identifier}; }}{Environment.NewLine}" +
                           $"\t set {{ {m.Namespace}.{m.Api}.{d.Identifier} = value; }}{Environment.NewLine}}}";

                case MemberKind.PropertyGetOnly:
                    return $"{d.ReturnType} {m.Interface}.{d.Identifier}{d.ArgsString}{Environment.NewLine}" +
                           $"\t => {m.Namespace}.{m.Api}.{d.Identifier};";

                default:
                    throw new NotImplementedException($"Don't know how to generate forwarding implementation for {d.Kind}");
            }
        }
    }
}