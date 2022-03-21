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
        private const string Source = "C:\\Downloads\\notepadplusplus.zip";
        private const string Backup = "C:\\local_bucket\\packages\\notepadplusplus_backup.zip";

        [Fact]
        public void CreateBackup()
        {
            var fileSystem = new MockFileSystem();
            fileSystem.AddFile(Destination, new MockFileData(new byte[0]));
            var copyInstaller = new CopyInstaller(Source, Destination, fileSystem);
            copyInstaller.Execute();
            fileSystem.FileExists(Backup);
            fileSystem.FileExists(Destination);
        }

        [Fact]
        public async void CopyFile()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { { "notepadplusplus.zip", new MockFileData(new byte[64]) } }, "C:\\Downloads");
            var copyInstaller = new CopyInstaller(Source, Destination, fileSystem);
            await copyInstaller.Execute();
            Assert.True(fileSystem.FileExists(Destination));
        }

        [Fact]
        public void RestoreBackupWhenUndo()
        {
            var mockFileSystem = new MockFileSystem();
            var file = Substitute.For<IFile>();
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.Path.Returns(mockFileSystem.Path);
            fileSystem.File.Returns(file);
            var copyInstaller = new CopyInstaller(Source, Destination, fileSystem);
            copyInstaller.Undo();

            file.Received().Copy(Backup, Destination, true);
        }
    }
}
