using DgSystems.PackageManager.Install;
using DgSystems.Scoop;
using NSubstitute;
using System.IO.Abstractions;
using Xunit;

namespace DgSystems.ScoopUnitTests
{
    public class BucketShould
    {
        CommandLineShell console = Substitute.For<CommandLineShell>();
        IFile file = Substitute.For<IFile>();

        [Fact]
        public void CopyManifestFiles()
        {
            string bucketPath = "C://my_bucket";
            Bucket bucket = new Bucket("my_bucket", bucketPath, console, file);
            Package package = new Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            bucket.Sync(package);

            file.Received().Copy("C://temp/notepad-plus-plus/notepad-plus-plus.json", $"{bucketPath}/manifests/notepad-plus-plus.json");
        }
    }
}
