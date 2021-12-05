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
        private const string packageUrl = "http://localhost/packages/notepad-plus-plus.zip";
        private const string bucketRoot = "C://my_bucket";
        private const string packageDownloadedPath = "C://downloads/notepad-plus-plus.zip";

        readonly CommandLineShell console = Substitute.For<CommandLineShell>();
        readonly Downloader downloader = Substitute.For<Downloader>();
        readonly IFile file = Substitute.For<IFile>();

        [Fact]
        public void DownloadPackage()
        {
            Package package = new Package("notepad-plus-plus", packageUrl);
            BucketMock bucket = new BucketMock("my_bucket", bucketRoot, console, file, downloader);
            bucket.Sync(package);

            downloader.Received().DownloadFile(new Uri(packageUrl), packageDownloadedPath);
        }

        [Fact]
        public void UnzipPackage()
        {
            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            BucketMock bucket = new BucketMock("my_bucket", bucketRoot, console, file, downloader);
            bucket.Sync(package);

            Assert.Equal(packageDownloadedPath, bucket.SourceArchiveFileName);
            Assert.Equal(extractedTempFolder, bucket.DestinationDirectoryName);
        }

        [Fact]
        public void CopyManifestFiles()
        {
            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            Bucket bucket = new BucketMock("my_bucket", bucketRoot, console, file, downloader);
            bucket.Sync(package);

            file.Received().Copy($"{extractedTempFolder}/notepad-plus-plus.json", $"{bucketRoot}/manifests/notepad-plus-plus.json");
        }
    }
}
