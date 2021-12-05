using DgSystems.PackageManager.Install;
using DgSystems.Scoop;
using NSubstitute;
using System;
using System.IO.Abstractions;
using Xunit;

namespace DgSystems.ScoopUnitTests
{
    public class BucketShould
    {
        private const string extractedTempFolder = "C://temp/notepad-plus-plus";
        CommandLineShell console = Substitute.For<CommandLineShell>();
        IFile file = Substitute.For<IFile>();
        ZipFile zipFile = Substitute.For<ZipFile>();

        [Fact]
        public void UnzipPackage()
        {
            string bucketPath = "C://my_bucket";
            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            Bucket bucket = new Bucket("my_bucket", bucketPath, console, file, zipFile); // doing too much, too many dependencies
            string packageDownloadedPath = "C://downloads/notepad-plus-plus.zip";
            bucket.Sync(package);

            zipFile.Received().ExtractToDirectory(packageDownloadedPath, extractedTempFolder);
        }

        [Fact]
        public void CopyManifestFiles()
        {
            string bucketPath = "C://my_bucket";
            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            Bucket bucket = new Bucket("my_bucket", bucketPath, console, file, zipFile);
            bucket.Sync(package);

            file.Received().Copy($"{extractedTempFolder}/notepad-plus-plus.json", $"{bucketPath}/manifests/notepad-plus-plus.json");
        }
    }
}
