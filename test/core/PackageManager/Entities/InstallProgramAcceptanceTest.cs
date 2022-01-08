using DgSystems.Downloader;
using DgSystems.PackageManager;
using DgSystems.PackageManager.Entities;
using DgSystems.PackageManager.Entities.Events;
using DgSystems.PackageManager.WebAPI.Install;
using DgSystems.PowerShell;
using DgSystems.Scoop;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using System.Net.Http;
using Xunit;

namespace DgSystems.PackageManagerUnitTests.Entities
{
    public class InstallProgramAcceptanceTest
    {

        [Fact]
        public async void InstallSimpleProgramAsync()
        {
            // Arrange

            // External
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile("C:/temp/notepadplusplus/notepadplusplus.json", new MockFileData(new byte[1]));
            mockFileSystem.AddFile("C:/temp/notepadplusplus/notepadplusplus.zip", new MockFileData(new byte[1]));
            var downloadContent = new MultipartContent("zip") {
                new ByteArrayContent(new byte[64])
            };
            var httpResponseMessage = new HttpResponseMessage() { 
                StatusCode = HttpStatusCode.OK, Content = downloadContent
            };
            var httpClient = new HttpClient(new MockHttpMessageHandler(httpResponseMessage));
            var logger = Substitute.For<ILogger<LoggerNotifier>>();
            static void extractZip(string x, string y) => Console.Write("");
            var process = Substitute.For<Process>();

            // Collaborators
            var program = new Package("notepadplusplus", "C:\\setup.exe", "setup.zip");
            var powershellFactory = new PowerShellFactory(process);
            var downloader = new DownloadManager(httpClient, mockFileSystem);
            var scoopFactory = new ScoopFactory(powershellFactory, mockFileSystem, downloader, extractZip);
            var packageManager = scoopFactory.Create();
            var notifier = new LoggerNotifier(logger);
            var installation = new Installation(packageManager, notifier);
            
            // Act

            var installationStatus = await installation.Install(program);

            // Assert

            installationStatus.Should().Be(InstallationStatus.Success);
        }

        [Fact]
        public async void InstallPackageWithDependenciesAsync()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe", "java.zip");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", "eclipse.zip", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Entities.PackageManager>();

            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Success);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);


            Received.InOrder(() =>
            {
                packageManager.Received().Install(dependencyPackage);
                packageManager.Received().Install(mainPackage);
            });

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationExecuted(installation.Id, dependencyPackage.Name));
                notifier.Received().Notify(new InstallationExecuted(installation.Id, mainPackage.Name));
            });
        }

        [Fact]
        public async void DontInstallPackageWithDependenciesIfDependencyInstallationFailsAsync()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe", "java.zip");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", "eclipse.zip", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Entities.PackageManager>();

            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Failure);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            await packageManager.Received().Install(dependencyPackage);
            await packageManager.DidNotReceive().Install(mainPackage);

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationFailed(installation.Id, dependencyPackage.Name, $"Installation failed for package {dependencyPackage.Name}"));
                notifier.Received().Notify(new InstallationFailed(installation.Id, mainPackage.Name, $"Dependency not installed."));
            });
        }
    }
}
