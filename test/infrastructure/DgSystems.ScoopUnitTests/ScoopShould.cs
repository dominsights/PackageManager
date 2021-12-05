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
            var bucketList = new BucketList();
            var bucket = new Bucket(console, "my_bucket");
            var package = new PackageManager.Install.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, bucketList);

            await scoop.Install(package);
            bucket.Received().Sync(package);
        }
    }
}
