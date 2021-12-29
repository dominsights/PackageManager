using DgSystems.Scoop;
using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
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
            var commandLine = Substitute.For<CommandLineShell>();
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { { "notepadplusplus.json", new MockFileData(new byte[64]) } }, downloadFolder);
            var copyManifest = new CopyManifest(fileSystem, "C:\\Downloads\\notepadplusplus.json", "C:\\local_bucket\\manifests\\notepadplusplus.json", commandLine);
            await copyManifest.Execute();

            Assert.True(fileSystem.FileExists("C:\\local_bucket\\manifests\\notepadplusplus.json"));
        }

        [Fact]
        public async void CopyFileWhenFileAlreadyExists()
        {
            var commandLine = Substitute.For<CommandLineShell>();
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { { "notepadplusplus.json", new MockFileData(new byte[64]) } }, downloadFolder);
            fileSystem.AddFile("C:\\local_bucket\\manifests\\notepadplusplus.json", new MockFileData(new byte[64]));
            var copyManifest = new CopyManifest(fileSystem, "C:\\Downloads\\notepadplusplus.json", "C:\\local_bucket\\manifests\\notepadplusplus.json", commandLine);
            await copyManifest.Execute();

            Assert.True(fileSystem.FileExists("C:\\local_bucket\\manifests\\notepadplusplus.json"));
            Assert.True(fileSystem.AllFiles.Count() == 2);
        }

        [Fact]
        public async void UndoCopy()
        {
            var commandLine = Substitute.For<CommandLineShell>();
            var fileSystem = Substitute.For<IFileSystem>();
            var copyManifest = new CopyManifest(fileSystem, "C:\\Downloads\\notepadplusplus.json", "C:\\local_bucket\\manifests\\notepadplusplus.json", commandLine);
            await Assert.ThrowsAsync<Exception>(() => copyManifest.Execute());

            await commandLine.Received().Execute(new List<string>
            {
                "git reset",
                "git checkout .",
                "git clean -fdx"
            });
        }
    }
}
