using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace DgSystems.ScoopUnitTests.Commands
{
    public class CopyInstallerShould
    {
        private const string Destination = "C:\\local_bucket\\packages\\notepadplusplus.zip";

        [Fact]
        public void CreateBackup()
        {
            var fileSystem = new MockFileSystem();
            fileSystem.AddFile(Destination, new MockFileData(new byte[0]));
            var copyInstaller = new CopyInstaller("C:\\Downloads\\notepadplusplus.zip", Destination, fileSystem);
            copyInstaller.Execute();
            fileSystem.FileExists("C:\\local_bucket\\packages\\notepadplusplus_backup.zip");
            fileSystem.FileExists("C:\\local_bucket\\packages\\notepadplusplus.zip");
        }

        [Fact]
        public async void CopyFile()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { { "notepadplusplus.zip", new MockFileData(new byte[64]) } }, "C:\\Downloads");
            var copyInstaller = new CopyInstaller("C:\\Downloads\\notepadplusplus.zip", Destination, fileSystem);
            await copyInstaller.Execute();
            Assert.True(fileSystem.FileExists("C:\\local_bucket\\packages\\notepadplusplus.zip"));
        }
    }
}
