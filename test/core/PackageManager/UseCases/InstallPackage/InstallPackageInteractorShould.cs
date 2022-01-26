using DgSystems.PackageManager;
using DgSystems.PackageManager.Entities;
using DgSystems.PackageManager.UseCases.InstallPackage;
using NSubstitute;
using Xunit;

namespace DgSystems.PackageManagerUnitTests.UseCases.InstallPackage
{
    public class InstallPackageInteractorShould
    {
        [Fact]
        public async void InstallPackage()
        {
            var presenter = Substitute.For<InstallPackageOutputBoundary>();

            var packageManager = Substitute.For<PackageManager.Entities.PackageManager>();
            var package = new Package("notepad++", "C:\\setup.exe", "setup.zip");
            packageManager.IsPackageValid(package).Returns(true);
            packageManager.Install(package).Returns(InstallationStatus.Success);

            var notifier = Substitute.For<Notifier>();
            var request = new InstallPackageRequest("notepad++", "C:\\setup.exe", "setup.zip");
            var installPackageInteractor = new InstallPackageInteractor(presenter, packageManager, notifier);
            await installPackageInteractor.ExecuteAsync(request);

            presenter.Received().PresentAsync(new InstallPackageResponse("notepad++", "notepad++ was installed successfully."));
        }

        [Fact]
        public async void ReturnFailMessage()
        {
            var presenter = Substitute.For<InstallPackageOutputBoundary>();

            var packageManager = Substitute.For<PackageManager.Entities.PackageManager>();
            var package = new Package("notepad++", "C:\\setup.exe", "setup.zip");
            packageManager.IsPackageValid(package).Returns(true);
            packageManager.Install(package).Returns(InstallationStatus.Failure);

            var notifier = Substitute.For<Notifier>();
            var request = new InstallPackageRequest("notepad++", "C:\\setup.exe", "setup.zip");
            var installPackageInteractor = new InstallPackageInteractor(presenter, packageManager, notifier);
            await installPackageInteractor.ExecuteAsync(request);

            presenter.Received().PresentAsync(new InstallPackageResponse("notepad++", "notepad++ failed to install."));
        }
    }
}
