using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DgSystems.ScoopUnitTests.Commands
{
    public class DownloadPackageShould
    {
        private const string DownloadFolder = "C:\\Downloads";
        private const string FileName = "notepadplusplus.zip";
        private const string FilePath = "C:\\Downloads\\notepadplusplus.zip";

        [Fact]
        public async void Download()
        {
            var fileSystem = new MockFileSystem();
            var downloader = Substitute.For<Scoop.Downloader>();
            var uri = new Uri("http://localhost/notepadplusplus.zip");
            var downloadPackage = new DownloadPackage(downloader, uri, DownloadFolder, fileSystem);
            await downloadPackage.Execute();

            await downloader.Received().DownloadFile(uri, DownloadFolder);
        }

        [Fact]
        public void DeleteDownloadedFile()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { { FileName, new MockFileData(new byte[64]) } }, DownloadFolder);
            var downloader = Substitute.For<Scoop.Downloader>();
            var uri = new Uri($"http://localhost/{FileName}");
            var downloadPackage = new DownloadPackage(downloader, uri, DownloadFolder, fileSystem);
            downloadPackage.Undo();
            Assert.False(fileSystem.FileExists(FilePath));
            Assert.False(fileSystem.AllFiles.Any());
        }
    }
}
