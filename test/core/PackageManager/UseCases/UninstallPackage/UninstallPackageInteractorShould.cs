using DgSystems.PackageManager.Entities;
using DgSystems.PackageManager.UseCases.UninstallPackage;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DgSystems.PackageManagerUnitTests.UseCases.UninstallPackage
{
    public class UninstallPackageInteractorShould
    {
        [Fact]
        public async Task UninstallPackageAsync()
        {
            var request = new UninstallPackageRequest("notepadplusplus");
            var presenter = Substitute.For<UninstallPackageOutputBoundary>();
            var uninstallation = Substitute.For<PackageUninstallation>();
            var interactor = new UninstallPackageInteractor(presenter, uninstallation);
            await interactor.ExecuteAsync(request);

            await uninstallation.Received().Uninstall("notepadplusplus");
            presenter.Received().PresentAsync(new UninstallPackageResponse("notepadplusplus was uninstalled successfully."));
        }

        [Fact]
        public async Task ReturnFailMessage()
        {
            var request = new UninstallPackageRequest("notepadplusplus");
            var presenter = Substitute.For<UninstallPackageOutputBoundary>();
            var uninstallation = Substitute.For<PackageUninstallation>();
            uninstallation.When(x => x.Uninstall("notepadplusplus")).Do(x => throw new Exception());
            var interactor = new UninstallPackageInteractor(presenter, uninstallation);
            await interactor.ExecuteAsync(request);

            presenter.Received().PresentAsync(new UninstallPackageResponse("notepadplusplus failed to uninstall."));
        }

        [Fact]
        public async Task Return_success_when_uninstall_works()
        {
            var request = new UninstallPackageRequest("notepadplusplus");
            var presenter = Substitute.For<UninstallPackageOutputBoundary>();
            var uninstallation = Substitute.For<PackageUninstallation>();
            uninstallation.Uninstall("notepadplusplus").Returns(UninstallationStatus.Success);
            var interactor = new UninstallPackageInteractor(presenter, uninstallation);
            await interactor.ExecuteAsync(request);

            await uninstallation.Received().Uninstall("notepadplusplus");
            presenter.Received().PresentAsync(new UninstallPackageResponse("notepadplusplus was uninstalled successfully."));
        }

        [Fact]
        public async Task Return_failure_when_uninstall_fails()
        {
            var request = new UninstallPackageRequest("notepadplusplus");
            var presenter = Substitute.For<UninstallPackageOutputBoundary>();
            var uninstallation = Substitute.For<PackageUninstallation>();
            uninstallation.Uninstall("notepadplusplus").Returns(UninstallationStatus.Failure);
            var interactor = new UninstallPackageInteractor(presenter, uninstallation);
            await interactor.ExecuteAsync(request);

            await uninstallation.Received().Uninstall("notepadplusplus");
            presenter.Received().PresentAsync(new UninstallPackageResponse("notepadplusplus failed to uninstall."));
        }
    }
}
