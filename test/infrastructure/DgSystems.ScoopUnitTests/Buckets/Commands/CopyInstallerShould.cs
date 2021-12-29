using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace DgSystems.ScoopUnitTests.Commands
{
    public class CopyInstallerShould
    {
        private const string Destination = "C:\\local_bucket\\notepadplusplus.zip";

        [Fact]
        public void CreateBackup()
        {
            var fileSystem = new MockFileSystem();
            fileSystem.AddFile(Destination, new MockFileData(new byte[0]));
            var copyInstaller = new CopyInstaller("C:\\Downloads\\notepadplusplus.zip", Destination, fileSystem);
            copyInstaller.Execute();
            fileSystem.FileExists("C:\\local_bucket\\notepadplusplus_backup.zip");
            fileSystem.FileExists("C:\\local_bucket\\notepadplusplus.zip");
        }
    }
}
