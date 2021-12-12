using DgSystems.Scoop;
using DgSystems.Scoop.Buckets;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ScoopClass = DgSystems.Scoop.Scoop;

namespace DgSystems.ScoopUnitTests
{
    public class InstallScoopPackageAcceptanceTest
    {
        // create bucket if it doesn't exist yet
        // download package from path provided
        // update bucket with new package
        // install package

        [Fact]
        public void InstallScoopPackage()
        {
            var console = Substitute.For<CommandLineShell>();
            var file = Substitute.For<IFile>();
            var bucketList = new BucketList();
            var bucketPath = "C://my_bucket";
            string downloadFolder = "C://downloads";

            var bucket = new Bucket("my_bucket", bucketPath, console, file, Substitute.For<Downloader>(), new BucketCommandFactory());
            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, bucketList, downloadFolder, (source, destination) => ZipFile.ExtractToDirectory(source, destination));
            scoop.Install(new PackageManager.Install.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip", "notepad-plus-plus.zip"));
            console.Received().Execute("scoop install notepad-plus-plus");
        }
    }
}
