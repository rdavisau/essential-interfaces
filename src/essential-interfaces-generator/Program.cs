using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using EssentialInterfaces.Models;
using EssentialInterfaces.Tasks;

namespace EssentialInterfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = GetContext(args);

            var apiModels = new RoslynModelGenerator().Generate(context);
            var generatedCode = new ImplementationGenerator().Generate(context, apiModels);
            var success = new ProjectMutator().Mutate(context, generatedCode);

            Console.WriteLine(
                $"Generated interfaces for {apiModels.Count} types:{Environment.NewLine}" +
                String.Join(Environment.NewLine, apiModels.Select(x => $" - {x.Api} ({x.Declarations.Count} members)")));

            if (Debugger.IsAttached)
                Console.ReadLine();
        }

        private static GeneratorContext GetContext(string [] args)
        {
            if ((args?.Length ?? 0) < 2)
                throw new Exception("Provide path to Xamarin.Essentials root and the Essential.Interfaces root to run.");

            var (essentialsRoot, outputProjectRoot) = (args[0], args[1]);

            if (!Directory.Exists(essentialsRoot))
                throw new Exception($"Didn't find Xamarin.Essentials implementations at ${essentialsRoot}.");

            if (!Directory.Exists(outputProjectRoot))
                throw new Exception($"Didn't find Essential.Interfaces project at {outputProjectRoot}.");

            return new GeneratorContext(essentialsRoot, outputProjectRoot);
        }
    }
}