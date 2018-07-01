using System;
using System.IO;
using System.Linq;
using EssentialInterfaces.Tasks;
using LibGit2Sharp;

namespace EssentialInterfaces.Models
{
    public class GeneratorContext
    {
        // inputs
        public string XamarinEssentialsRepoPath { get; }
        public string XamarinEssentialsCommitSha { get; set; }
        public string XamarinEssentialsPackageVersion { get; set; }
        public string EssentialInterfacesProjectPath { get; }

        public string XamarinEssentialsImplementationsPath 
            => Path.Combine(XamarinEssentialsRepoPath, "Xamarin.Essentials");

        public GeneratorContext(string essentialsRoot, string outputProjectRoot)
        {
            XamarinEssentialsRepoPath = essentialsRoot;
            EssentialInterfacesProjectPath = outputProjectRoot;

            XamarinEssentialsCommitSha = GetEssentialsCommitSha();
            XamarinEssentialsPackageVersion = GetEssentialsPackageVersion();

            Console.WriteLine(
                $"Using Essentials repo at {XamarinEssentialsRepoPath} (SHA {XamarinEssentialsCommitSha})" +
                Environment.NewLine +
                $"Current version of Xamarin.Essentials nuget is {XamarinEssentialsPackageVersion}"
                + Environment.NewLine +
                $"Writing to Essential.Interfaces placeholder at {EssentialInterfacesProjectPath}");
        }

        private string GetEssentialsCommitSha()
        {
            using (var repo = new Repository(XamarinEssentialsRepoPath))
                return repo.Commits.First().Sha;
        }

        private string GetEssentialsPackageVersion()
        {
            var pmp = new NugetPackageMetadataProvider();
            var package = pmp.GetLatestVersionForPackage("Xamarin.Essentials");

            return $"{package.Version}";
        }
    }
}