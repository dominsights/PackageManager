using DgSystems.PackageManager.Install;
using DgSystems.Scoop;
using DgSystems.Scoop.Buckets;
using NSubstitute;
using System;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Threading.Tasks;
using Xunit;
using ScoopClass = DgSystems.Scoop.Scoop;

namespace DgSystems.ScoopUnitTests
{
    public class InstallScoopPackageAcceptanceTest
    {
        [Fact]
        public async Task InstallScoopPackage()
        {
            var console = Substitute.For<CommandLineShell>();
            var file = Substitute.For<IFile>();
            var bucketList = new BucketList();
            var bucketPath = "C://my_bucket";
            string downloadFolder = "C://downloads";

            var downloadPackage = Substitute.For<Command>();
            var extractPackage = Substitute.For<Command>();
            var copyManifest = Substitute.For<Command>();
            var syncGitRepository = Substitute.For<Command>();
            var copyInstaller = Substitute.For<Command>();
            var commandFactory = Substitute.For<BucketCommandFactory>();

            commandFactory.CreateDownloadPackageCommand(Arg.Any<Downloader>(), Arg.Any<Uri>(), Arg.Any<string>()).Returns(downloadPackage);
            commandFactory.CreateExtractPackageCommand(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ExtractToDirectory>()).Returns(extractPackage);
            commandFactory.CreateCopyManifestCommand(Arg.Any<IFile>(), Arg.Any<string>(), Arg.Any<string>()).Returns(copyManifest);
            commandFactory.CreateSyncGitRepositoryCommand(Arg.Any<string>(), Arg.Any<CommandLineShell>()).Returns(syncGitRepository);
            commandFactory.CreateCopyInstallerCommand(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IFile>()).Returns(copyInstaller);

            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip", "notepad-plus-plus.zip");
            var bucket = new Bucket("my_bucket", bucketPath, console, file, Substitute.For<Downloader>(), commandFactory);

            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, bucketList, downloadFolder, (source, destination) => ZipFile.ExtractToDirectory(source, destination));
            var result = await scoop.Install(package);
            await console.Received().Execute("scoop install notepad-plus-plus");
            Assert.Equal(InstallationStatus.Success, result);
        }
    }
}
