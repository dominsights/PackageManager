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
            var presenter = Substitute.For<OutputBoundary>();

            var packageManager = Substitute.For<PackageManager.Entities.PackageManager>();
            var package = new Package("notepad++", "C:\\setup.exe", "setup.zip");
            packageManager.IsPackageValid(package).Returns(true);
            packageManager.Install(package).Returns(InstallationStatus.Success);

            var notifier = Substitute.For<Notifier>();
            var request = new Request("notepad++", "C:\\setup.exe", "setup.zip");
            var installPackageInteractor = new Interactor(presenter, packageManager, notifier);
            await installPackageInteractor.ExecuteAsync(request);

            presenter.Received().Execute(new Response("notepad++", "notepad++ was installed successfully."));
        }

        [Fact]
        public async void ReturnFailMessage()
        {
            var presenter = Substitute.For<OutputBoundary>();

            var packageManager = Substitute.For<PackageManager.Entities.PackageManager>();
            var package = new Package("notepad++", "C:\\setup.exe", "setup.zip");
            packageManager.IsPackageValid(package).Returns(true);
            packageManager.Install(package).Returns(InstallationStatus.Failure);

            var notifier = Substitute.For<Notifier>();
            var request = new Request("notepad++", "C:\\setup.exe", "setup.zip");
            var installPackageInteractor = new Interactor(presenter, packageManager, notifier);
            await installPackageInteractor.ExecuteAsync(request);

            presenter.Received().Execute(new Response("notepad++", "notepad++ failed to install."));
        }
    }
}
