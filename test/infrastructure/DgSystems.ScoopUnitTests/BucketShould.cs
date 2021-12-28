using DgSystems.PackageManager.Entities;
using DgSystems.Scoop;
using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
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
        private const string packageFileName = $"{ packageName }.zip";

        readonly CommandLineShell console = Substitute.For<CommandLineShell>();
        readonly Scoop.Downloader downloader = Substitute.For<Scoop.Downloader>();
        readonly IFile file = Substitute.For<IFile>();
        private string SourceArchiveFileName;
        private string DestinationDirectoryName;

        [Fact]
        public async Task DownloadPackage()
        {
            Package package = new Package(packageName, packageUrl, packageFileName);
            Bucket bucket = new Bucket(bucketName, bucketRoot, console, file, downloader, new CommandFactory());

            await bucket.Sync(package, downloadFolder, (x, y) => Console.Write(""));

            await downloader.Received().DownloadFile(new Uri(packageUrl), downloadFolder);
        }

        [Fact]
        public async Task UnzipPackage()
        {
            Package package = new Package(packageName, packageUrl, packageFileName);
            Bucket bucket = new Bucket(bucketName, bucketRoot, console, file, downloader, new CommandFactory());
            await bucket.Sync(package, downloadFolder, (x, y) => { SourceArchiveFileName = x; DestinationDirectoryName = y; });

            Assert.Equal(packageDownloadedPath, SourceArchiveFileName);
            Assert.Equal(extractedTempFolder, DestinationDirectoryName);
        }

        [Fact]
        public async Task CopyManifestFiles()
        {
            Package package = new Package(packageName, packageUrl, packageFileName);
            Bucket bucket = new Bucket(bucketName, bucketRoot, console, file, downloader, new CommandFactory());
            await bucket.Sync(package, downloadFolder, (x, y) => Console.Write(""));

            file.Received().Copy($"{extractedTempFolder}/{packageName}.json", $"{bucketRoot}/manifests/{packageName}.json", true);
        }

        [Fact]
        public async Task SyncGitRepository()
        {
            Package package = new Package(packageName, packageUrl, packageFileName);
            Bucket bucket = new Bucket(bucketName, bucketRoot, console, file, downloader, new CommandFactory());
            await bucket.Sync(package, downloadFolder, (x, y) => Console.Write(""));

            string moveToFolder = $"cd {bucketRoot}/manifests";
            string gitAdd = "git add .";
            string gitCommit = "git commit -m \"Sync\"";
            string scoopUpdate = "scoop update";
            await console.Received().Execute(Arg.Is<List<string>>(x => x.SequenceEqual(new List<string> { moveToFolder, gitAdd, gitCommit, scoopUpdate })));
        }

        [Fact]
        public async Task CopySetupFiles()
        {
            Package package = new Package(packageName, packageUrl, packageFileName);
            Bucket bucket = new Bucket(bucketName, bucketRoot, console, file, downloader, new CommandFactory());
            await bucket.Sync(package, downloadFolder, (x, y) => Console.Write(""));

            file.Received().Copy($"{extractedTempFolder}/{packageName}.zip", $"{bucketRoot}/packages/{packageName}.zip", true);
        }

        [Fact]
        public async Task UndoAllCommandsWhenCopyInstallerFails()
        {
            var downloadPackage = Substitute.For<Command>();
            var extractPackage = Substitute.For<Command>();
            var copyManifest = Substitute.For<Command>();
            var syncGitRepository = Substitute.For<Command>();
            var copyInstaller = Substitute.For<Command>();
            var commandFactory = Substitute.For<CommandFactory>();

            commandFactory.CreateDownloadPackage(Arg.Any<Scoop.Downloader>(), Arg.Any<Uri>(), Arg.Any<string>()).Returns(downloadPackage);
            commandFactory.CreateExtractPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ExtractToDirectory>()).Returns(extractPackage);
            commandFactory.CreateCopyManifest(Arg.Any<IFile>(), Arg.Any<string>(), Arg.Any<string>()).Returns(copyManifest);
            commandFactory.CreateSyncGitRepository(Arg.Any<string>(), Arg.Any<CommandLineShell>()).Returns(syncGitRepository);
            commandFactory.CreateCopyInstaller(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IFile>()).Returns(copyInstaller);

            copyInstaller
                .When(x => x.Execute())
                .Do(x => { throw new Exception(); });

            Package package = new Package(packageName, packageUrl, packageFileName);
            Bucket bucket = new Bucket(bucketName, bucketRoot, console, file, downloader, commandFactory);
            bool result = await bucket.Sync(package, downloadFolder, (x, y) => Console.Write(""));

            Assert.False(result);
            Received.InOrder(() =>
            {
                copyInstaller.Undo();
                syncGitRepository.Undo();
                copyManifest.Undo();
                extractPackage.Undo();
                downloadPackage.Undo();
            });
        }

        [Fact]
        public async Task ExecuteAllCommandsInOrder()
        {
            var downloadPackage = Substitute.For<Command>();
            var extractPackage = Substitute.For<Command>();
            var copyManifest = Substitute.For<Command>();
            var syncGitRepository = Substitute.For<Command>();
            var copyInstaller = Substitute.For<Command>();
            var commandFactory = Substitute.For<CommandFactory>();

            commandFactory.CreateDownloadPackage(Arg.Any<Scoop.Downloader>(), Arg.Any<Uri>(), Arg.Any<string>()).Returns(downloadPackage);
            commandFactory.CreateExtractPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ExtractToDirectory>()).Returns(extractPackage);
            commandFactory.CreateCopyManifest(Arg.Any<IFile>(), Arg.Any<string>(), Arg.Any<string>()).Returns(copyManifest);
            commandFactory.CreateSyncGitRepository(Arg.Any<string>(), Arg.Any<CommandLineShell>()).Returns(syncGitRepository);
            commandFactory.CreateCopyInstaller(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IFile>()).Returns(copyInstaller);

            Package package = new Package(packageName, packageUrl, packageFileName);
            Bucket bucket = new Bucket(bucketName, bucketRoot, console, file, downloader, commandFactory);
            bool result = await bucket.Sync(package, downloadFolder, (x, y) => Console.Write(""));

            Assert.True(result);
            Received.InOrder(() =>
            {
                downloadPackage.Execute();
                extractPackage.Execute();
                copyManifest.Execute();
                syncGitRepository.Execute();
                copyInstaller.Execute();
            });
        }
    }
}
