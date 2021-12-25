using DgSystems.PackageManager.Entities;
using DgSystems.Scoop;
using DgSystems.Scoop.Buckets;
using NSubstitute;
using System;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Xunit;
using ScoopClass = DgSystems.Scoop.Scoop;

namespace DgSystems.ScoopUnitTests
{
    public class ScoopShould
    {
        [Fact]
        public async Task UpdateManifestWhenNewPackageIsReceivedAsync()
        {
            var console = Substitute.For<CommandLineShell>();
            var bucketList = new BucketList();
            IFile file = Substitute.For<IFile>();
            string bucketPath = "C://my_bucket";
            string downloadFolder = "C://downloads";

            var bucket = Substitute.For<Bucket>();
            var package = new PackageManager.Entities.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip", "notepad-plus-plus.zip");
            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, bucketList, downloadFolder, (x, y) => Console.Write(""));

            await scoop.Install(package);
            await bucket.Received().Sync(package, downloadFolder, Arg.Any<ExtractToDirectory>());
        }

        [Fact]
        public async Task NotInstallPackageWhenBucketSyncFails()
        {
            var console = Substitute.For<CommandLineShell>();
            var bucketList = new BucketList();
            IFile file = Substitute.For<IFile>();
            string bucketPath = "C://my_bucket";
            string downloadFolder = "C://downloads";

            var bucket = Substitute.For<Bucket>();
            var package = new PackageManager.Entities.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip", "notepad-plus-plus.zip");
            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, bucketList, downloadFolder, (x, y) => Console.Write(""));

            var result = await scoop.Install(package);
            Assert.Equal(InstallationStatus.Failure, result);
            await console.DidNotReceive().Execute(Arg.Any<string>());
        }
    }
}
