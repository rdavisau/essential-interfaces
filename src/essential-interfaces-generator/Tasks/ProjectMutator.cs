using System.IO;
﻿using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using EssentialInterfaces.Models;

namespace EssentialInterfaces.Tasks
{
    public class ProjectMutator
    {
        public bool Mutate(GeneratorContext context, string generatedCode)
        {
            WriteEssentialsFile(context.EssentialInterfacesProjectPath, generatedCode);
            WriteCsproj(context.EssentialInterfacesProjectPath, context.XamarinEssentialsPackageVersion, context.XamarinEssentialsCommitSha);

            return true;
        }

        private void WriteEssentialsFile(string interfacesProjPath, string output)
        {
            var essentialsFilePath = Path.Combine(interfacesProjPath, "Essentials.cs");
            File.WriteAllText(essentialsFilePath, output);
        }

        private void WriteCsproj(string interfacesProjPath, string essentialsPackageVersion, string commitSha)
        {
            var csprojPath = Path.Combine(interfacesProjPath, "Essential.Interfaces.csproj");
            var xDoc = XDocument.Load(csprojPath);

            var nupkgVersion = GetNupkgVersion(essentialsPackageVersion);
            var releaseNotes = GetReleaseNotes(commitSha);

            SetEssentialsDependencyVersion(xDoc, essentialsPackageVersion);
            SetPropertyGroupValue(xDoc, "Version", nupkgVersion);
            SetPropertyGroupValue(xDoc, "PackageReleaseNotes", releaseNotes);

            xDoc.Save(csprojPath);
        }

        private string GetNupkgVersion(string essentialsPackageVersion) =>
            !String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("FORCE_NUPKG_VERSION"))
            ? Environment.GetEnvironmentVariable("FORCE_NUPKG_VERSION")
            : essentialsPackageVersion;

        private string GetReleaseNotes(string commitSha)
        {
            var additionalReleaseNotes = Environment.GetEnvironmentVariable("ADDITIONAL_RELEASENOTES");
            var releaseNotes = $"Generated from Xamarin.Essentials commit {commitSha}";
            
            if (!String.IsNullOrWhiteSpace(additionalReleaseNotes))
                releaseNotes += Environment.NewLine + Environment.NewLine + additionalReleaseNotes;
            
            return releaseNotes;
        }

        private void SetEssentialsDependencyVersion(XDocument doc, string version)
        {
            // only good while there's a single packageref 
            var element = doc.XPathSelectElement($"/Project[@Sdk=\"Microsoft.NET.Sdk\"]/ItemGroup/PackageReference");
            element.SetAttributeValue("Version", version);
        }

        private void SetPropertyGroupValue(XDocument doc, string name, string value)
        {
            var element = doc.XPathSelectElement($"/Project[@Sdk=\"Microsoft.NET.Sdk\"]/PropertyGroup/{name}");
            element.Value = value;
        }
    }
}