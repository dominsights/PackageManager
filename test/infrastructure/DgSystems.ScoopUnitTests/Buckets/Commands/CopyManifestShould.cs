using DgSystems.Scoop.Buckets.Commands;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Xunit;

namespace DgSystems.ScoopUnitTests.Commands
{
    public class CopyManifestShould
    {
        private const string downloadFolder = "C:\\Downloads";

        [Fact]
        public async void CopyFile()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { { "notepadplusplus.json", new MockFileData(new byte[64]) } }, downloadFolder);
            var copyManifest = new CopyManifest(fileSystem, "C:\\Downloads\\notepadplusplus.json", "C:\\local_bucket\\manifests\\notepadplusplus.json");
            await copyManifest.Execute();

            Assert.True(fileSystem.FileExists("C:\\local_bucket\\manifests\\notepadplusplus.json"));
        }

        [Fact]
        public async void CopyFileWhenFileAlreadyExists()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { { "notepadplusplus.json", new MockFileData(new byte[64]) } }, downloadFolder);
            fileSystem.AddFile("C:\\local_bucket\\manifests\\notepadplusplus.json", new MockFileData(new byte[64]));
            var copyManifest = new CopyManifest(fileSystem, "C:\\Downloads\\notepadplusplus.json", "C:\\local_bucket\\manifests\\notepadplusplus.json");
            await copyManifest.Execute();

            Assert.True(fileSystem.FileExists("C:\\local_bucket\\manifests\\notepadplusplus.json"));
            Assert.True(fileSystem.AllFiles.Count() == 2);
        }
    }
}
