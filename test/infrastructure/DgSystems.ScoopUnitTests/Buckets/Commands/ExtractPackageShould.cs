using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace DgSystems.ScoopUnitTests.Commands
{
    public class ExtractPackageShould
    {
        private const string SourceArchiveFileName = "C:\\Downloads\\notepadplusplus.zip";
        private const string DestinationDirectoryName = "C:\\temp";

        [Fact]
        public async void Extract()
        {
            var fileSystem = Substitute.For<IFileSystem>();
            string source = "", destination = "";
            void extract(string x, string y)
            {
                source = x;
                destination = y;
            }
            var extractPackage = new ExtractPackage(SourceArchiveFileName, DestinationDirectoryName, extract, fileSystem);
            await extractPackage.Execute();

            Assert.Equal(SourceArchiveFileName, source);
            Assert.Equal(DestinationDirectoryName, destination);
        }

        [Fact]
        public async void DeleteExtractedFolder()
        {
            var fileSystem = Substitute.For<IFileSystem>();
            var mockFileSystem = new MockFileSystem();
            fileSystem.Path.Returns(mockFileSystem.Path);
            fileSystem.Directory.Returns(Substitute.For<IDirectory>());
            var extractPackage = new ExtractPackage(SourceArchiveFileName, DestinationDirectoryName, (x, y) => Console.Write(""), fileSystem);
            await extractPackage.Undo();

            fileSystem.Directory.Received().Delete("C:\\temp\\notepadplusplus", true);
        }
    }
}
