using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EssentialInterfaces.Helpers;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace EssentialInterfaces.Tasks
{
    public class NugetPackageMetadataProvider
    {
        // t h a n k you daveaglick
        public PackageSearchMetadata GetLatestVersionForPackage(string package)
        {
            var logger = new NugetLogger();

            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());

            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            var sourceRepository = new SourceRepository(packageSource, providers);
            var packageMetadataResource = sourceRepository.GetResourceAsync<PackageMetadataResource>().Result;

            var searchMetadata = 
                packageMetadataResource.GetMetadataAsync(package, true, true, logger, CancellationToken.None).Result;

            return 
                searchMetadata
                    .OfType<PackageSearchMetadata>()
                    .OrderByDescending(x => x.Published)
                    .First();
        }

        class NugetLogger : NuGet.Common.ILogger
        {
            public void LogDebug(string data) => $"DEBUG: {data}".Dump();
            public void LogVerbose(string data) => $"VERBOSE: {data}".Dump();
            public void LogInformation(string data) => $"INFORMATION: {data}".Dump();
            public void LogMinimal(string data) => $"MINIMAL: {data}".Dump();
            public void LogWarning(string data) => $"WARNING: {data}".Dump();
            public void LogError(string data) => $"ERROR: {data}".Dump();
            public void LogInformationSummary(string data) => $"SUMMARY: {data}".Dump();
            public void LogErrorSummary(string data) => $"ERROR SUMMARY: {data}".Dump();
        }
    }
}