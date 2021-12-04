using DgSystems.Scoop;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var repository = Substitute.For<Repository>();
            var bucketList = new BucketList();
            var bucket = new Bucket("my_bucket");
            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, repository, bucketList);
            var package = new PackageManager.Install.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");

            await scoop.InstallAsync(package);
            repository.Received().Sync(bucket, package);
        }
    }
}
