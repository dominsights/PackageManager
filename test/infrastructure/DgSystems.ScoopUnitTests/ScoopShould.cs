using DgSystems.Scoop;
using NSubstitute;
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


            var bucket = new Bucket("my_bucket", bucketPath, console, file, Substitute.For<Downloader>());
            var package = new PackageManager.Install.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, bucketList);

            await scoop.Install(package);
            bucket.Received().Sync(package, downloadFolder);
        }
    }
}
