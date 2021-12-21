using DgSystems.Downloader;
using FluentAssertions;
using System;
using System.IO.Abstractions.TestingHelpers;
using System.Net.Http;
using Xunit;

namespace DgSystems.DownloaderUnitTests
{
    public class DownloadManagerShould
    {
        [Fact]
        public async void Download()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK };
            var httpMessageHandler = new MockHttpMessageHandler(httpResponseMessage);
            var httpClient = new HttpClient(httpMessageHandler);
            var fileSystem = new MockFileSystem();
            fileSystem.AddDirectory("C:\\Downloads");
            var downloader = new DownloadManager(httpClient, fileSystem);

            // Act
            await downloader.DownloadFile(new Uri("http://localhost/eclipse.exe"), "C:\\Downloads");
            
            // Assert
            downloader.IsSuccess().Should().BeTrue();
        }
    }
}
