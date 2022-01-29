using DgSystems.Downloader;
using DgSystems.PackageManager.Controllers.UninstallPackage;
using DgSystems.PackageManager.Presenters;
using DgSystems.PackageManager.Presenters.UninstallPackage;
using DgSystems.PackageManager.UseCases.UninstallPackage;
using DgSystems.PackageManagerUnitTests.Features;
using DgSystems.PowerShell;
using DgSystems.Scoop;
using FluentAssertions;
using NSubstitute;
using System;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Xunit;

namespace DgSystems.PackageManagerUnitTests.Acceptance
{
    public class UninstallPackageAcceptanceTest : IClassFixture<HttpClientFixture>
    {
        readonly UninstallPackagePresenter uninstallPresenter = new UninstallPackagePresenter();
        readonly PresenterObserver observer = new PresenterObserver();
        UninstallPackageInteractor? uninstallInteractor;
        readonly ScoopFactory scoopFactory;

        public UninstallPackageAcceptanceTest(HttpClientFixture httpClientFixture)
        {
            Process process = Substitute.For<Process>();
            var powershellFactory = new PowerShellFactory(process);
            var downloader = new DownloadManager(httpClientFixture.HttpClient, Substitute.For<IFileSystem>());
            static void extractZip(string x, string y) => Console.Write("");
            scoopFactory = new ScoopFactory(powershellFactory, Substitute.For<IFileSystem>(), downloader, extractZip);
        }


        [Fact]
        public async Task ShouldUninstallPackageAsync()
        {
            // Given
            uninstallPresenter.Attach(observer);
            uninstallInteractor = new UninstallPackageInteractor(uninstallPresenter, scoopFactory.Create());
            var uninstallController = new UninstallController(uninstallInteractor);

            // When
            await uninstallController.UninstallAsync("notepadplusplus");

            // Then
            observer.Invoked.Should().BeTrue();
            Assert.NotNull(observer.Output);
            observer.Output?.Message.Should().Be("notepadplusplus uninstalled with success.");
        }

        private class PresenterObserver : Observer
        {
            public bool Invoked { get; set; }
            public UninstallPackageOutput? Output { get; set; }
            public void Update(Subject subject)
            {
                if (subject is UninstallPackagePresenter presenter)
                {
                    Output = presenter.Output;
                    Invoked = true;
                }
            }
        }
    }
}
