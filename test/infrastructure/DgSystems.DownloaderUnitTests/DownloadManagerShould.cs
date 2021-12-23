using DgSystems.Downloader;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using System.Net.Http;
using Xunit;

namespace DgSystems.DownloaderUnitTests
{
    public class DownloadManagerShould
    {
        private const string eclipseUrl = "http://localhost/eclipse.exe";
        private const string downloadFolder = "C:\\Downloads";

        [Fact]
        public async void Download()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
            var httpMessageHandler = new MockHttpMessageHandler(httpResponseMessage);
            var httpClient = new HttpClient(httpMessageHandler);
            var fileSystem = new MockFileSystem();
            fileSystem.AddDirectory(downloadFolder);
            var downloader = new DownloadManager(httpClient, fileSystem);

            // Act
            await downloader.DownloadFile(new Uri(eclipseUrl), downloadFolder);

            // Assert
            downloader.IsSuccess().Should().BeTrue();
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.NotAcceptable)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.RequestTimeout)]
        public async void NotCopyWhenDownloadFails(HttpStatusCode httpStatusCode)
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage() { StatusCode = httpStatusCode };
            var httpMessageHandler = new MockHttpMessageHandler(httpResponseMessage);
            var httpClient = new HttpClient(httpMessageHandler);
            var fileSystem = new MockFileSystem();
            fileSystem.AddDirectory(downloadFolder);
            var downloader = new DownloadManager(httpClient, fileSystem);

            // Act
            await Assert.ThrowsAsync<HttpRequestException>(() => downloader.DownloadFile(new Uri(eclipseUrl), downloadFolder));

            // Assert
            downloader.IsSuccess().Should().BeFalse();
        }

        [Fact]
        public async void OverrideWhenFileAlreadyExists()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
            var httpMessageHandler = new MockHttpMessageHandler(httpResponseMessage);
            var httpClient = new HttpClient(httpMessageHandler);
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { { "eclipse.exe", new MockFileData(new byte[64]) } }, downloadFolder);
            var downloader = new DownloadManager(httpClient, fileSystem);

            // Act
            await downloader.DownloadFile(new Uri(eclipseUrl), downloadFolder);

            // Assert
            downloader.IsSuccess().Should().BeTrue();
            fileSystem.AllFiles.Should().HaveCount(1);
        }

        [Fact]
        public async void FailWhenCopyFails()
        {
            // Arrange
            var file = Substitute.For<IFile>();
            file
                .When(x => x.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>()))
                .Do(x => { throw new UnauthorizedAccessException(); });

            file.Exists("C:\\Downloads\\" + "eclipse.exe").Returns(true);

            var fileSystem = Substitute.For<IFileSystem>();
            var mockFileSystem = new MockFileSystem();
            fileSystem.File.Returns(file);
            fileSystem.Path.Returns(mockFileSystem.Path);

            var httpResponseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
            var httpMessageHandler = new MockHttpMessageHandler(httpResponseMessage);
            var httpClient = new HttpClient(httpMessageHandler);
            var downloader = new DownloadManager(httpClient, fileSystem);

            // Act
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => downloader.DownloadFile(new Uri(eclipseUrl), downloadFolder));

            // Assert
            downloader.IsSuccess().Should().BeFalse();
        }
    }
}
