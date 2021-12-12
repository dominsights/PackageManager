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
            var package = new PackageManager.Install.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip", "notepad-plus-plus.zip");
            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, bucketList, downloadFolder, (x, y) => Console.Write(""));

            await scoop.Install(package);
            bucket.Received().Sync(package, downloadFolder, Arg.Any<ExtractToDirectory>());
        }
    }
}
