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

        [Fact]
        public void DownloadPackage()
        {
            string bucketPath = "C://my_bucket";
            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            BucketMock bucket = new BucketMock("my_bucket", bucketPath, console, file); // doing too much, too many dependencies
            string packageDownloadedPath = "C://downloads/notepad-plus-plus.zip";
            bucket.Sync(package);

            Assert.Equal(packageDownloadedPath, bucket.Address.AbsoluteUri);
            Assert.Equal(packageDownloadedPath, bucket.FileName);
        }

        [Fact]
        public void UnzipPackage()
        {
            string bucketPath = "C://my_bucket";
            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            BucketMock bucket = new BucketMock("my_bucket", bucketPath, console, file); // doing too much, too many dependencies
            string packageDownloadedPath = "C://downloads/notepad-plus-plus.zip";
            bucket.Sync(package);

            Assert.Equal(packageDownloadedPath, bucket.SourceArchiveFileName);
            Assert.Equal(extractedTempFolder, bucket.DestinationDirectoryName);
        }

        [Fact]
        public void CopyManifestFiles()
        {
            string bucketPath = "C://my_bucket";
            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            Bucket bucket = new BucketMock("my_bucket", bucketPath, console, file);
            bucket.Sync(package);

            file.Received().Copy($"{extractedTempFolder}/notepad-plus-plus.json", $"{bucketPath}/manifests/notepad-plus-plus.json");
        }
    }
}
