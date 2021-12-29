﻿using DgSystems.Scoop.Buckets.Commands;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
