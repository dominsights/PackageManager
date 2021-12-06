using DgSystems.PackageManager.Install;
using DgSystems.Scoop;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Xunit;

namespace DgSystems.ScoopUnitTests
{
    public class BucketShould
    {
        private const string extractedTempFolder = "C://temp/notepad-plus-plus";
        private const string packageUrl = "http://localhost/packages/notepad-plus-plus.zip";
        private const string bucketName = "my_bucket";
        private const string bucketRoot = $"C://{bucketName}";
        private const string downloadFolder = "C://downloads";
        private const string packageName = "notepad-plus-plus";
        private const string packageDownloadedPath = $"{downloadFolder}/{packageName}.zip";

        readonly CommandLineShell console = Substitute.For<CommandLineShell>();
        readonly Downloader downloader = Substitute.For<Downloader>();
        readonly IFile file = Substitute.For<IFile>();

        [Fact]
        public void DownloadPackage()
        {
            Package package = new Package(packageName, packageUrl);
            BucketMock bucket = new BucketMock(bucketName, bucketRoot, console, file, downloader);
            bucket.Sync(package, downloadFolder);

            downloader.Received().DownloadFile(new Uri(packageUrl), downloadFolder);
        }

        [Fact]
        public void UnzipPackage()
        {
            Package package = new Package(packageName, packageUrl);
            downloader.DownloadFile(new Uri(packageUrl), downloadFolder).Returns(packageDownloadedPath);
            BucketMock bucket = new BucketMock(bucketName, bucketRoot, console, file, downloader);
            bucket.Sync(package, downloadFolder);

            Assert.Equal(packageDownloadedPath, bucket.SourceArchiveFileName);
            Assert.Equal(extractedTempFolder, bucket.DestinationDirectoryName);
        }

        [Fact]
        public void CopyManifestFiles()
        {
            Package package = new Package(packageName, packageUrl);
            Bucket bucket = new BucketMock(bucketName, bucketRoot, console, file, downloader);
            bucket.Sync(package, downloadFolder);

            file.Received().Copy($"{extractedTempFolder}/{packageName}.json", $"{bucketRoot}/manifests/{packageName}.json");
        }

        [Fact]
        public void NotTryToUnzipAndCopyPackageWhenDownloadFails()
        {
            Package package = new Package(packageName, packageUrl);
            BucketMock bucket = new BucketMock(bucketName, bucketRoot, console, file, downloader);
            downloader.When(x => x.DownloadFile(Arg.Any<Uri>(), Arg.Any<string>())).Do(x => throw new Exception());
            Assert.Throws<Exception>(() => bucket.Sync(package, downloadFolder));

            Assert.Null(bucket.SourceArchiveFileName);
            Assert.Null(bucket.DestinationDirectoryName);
            file.DidNotReceive().Copy(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void SyncGitRepository()
        {
            Package package = new Package(packageName, packageUrl);
            Bucket bucket = new BucketMock(bucketName, bucketRoot, console, file, downloader);
            bucket.Sync(package, downloadFolder);

            string moveToFolder = $"cd {bucketRoot}/manifests";
            string gitAdd = "git add .";
            string gitCommit = "git commit -m \"Sync\"";
            console.Received().Execute(Arg.Is<List<string>>(x => x.SequenceEqual(new List<string> { moveToFolder, gitAdd, gitCommit })));
        }
    }
}
