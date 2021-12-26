using DgSystems.PackageManager.Entities;
using DgSystems.PackageManager.UseCases.InstallPackage;
using NSubstitute;
using Xunit;

namespace DgSystems.PackageManagerUnitTests.UseCases.InstallPackage
{
    public class InstallPackageInteractorShould
    {
        [Fact]
        public void InstallPackage()
        {
            var presenter = Substitute.For<InstallPackageOutputBoundary>();
            var package = new Package("notepad++", "C:\\setup.exe", "setup.zip");
            var request = new InstallPackageRequest(package);
            var installPackageInteractor = new InstallPackageInteractor(presenter);
            installPackageInteractor.Execute(request);

            presenter.Received().Execute(new InstallPackageResponse("notepad++", "notepad++ was installed successfully."));
        }
    }
}
