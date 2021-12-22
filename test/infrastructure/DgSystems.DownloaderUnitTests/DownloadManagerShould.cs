using DgSystems.Downloader;
using FluentAssertions;
using System;
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

        [Fact]
        public async void NotCopyWhenDownloadFails()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.NotFound };
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
    }
}
