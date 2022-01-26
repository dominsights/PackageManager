using DgSystems.PackageManager.Controllers.InstallPackage;
using DgSystems.PackageManager.UseCases.InstallPackage;
using NSubstitute;
using Xunit;

namespace DgSystems.PackageManager.ControllersUnitTests.InstallPackage
{
    public class InstallControllerShould
    {
        [Fact]
        public void StartUseCase()
        {
            var inputBoundary = Substitute.For<InstallPackageInputBoundary>();
            var installController = new InstallController(inputBoundary);
            installController.Install("notepad++", "C:\\setup.exe", "setup.zip");
            inputBoundary.Received().ExecuteAsync(new InstallPackageRequest("notepad++", "C:\\setup.exe", "setup.zip"));
        }

        [Fact]
        public void RejectInvalidInput()
        {
            var inputBoundary = Substitute.For<InstallPackageInputBoundary>();
            var installController = new InstallController(inputBoundary);
            installController.Install("", "C:\\setup.exe", "setup.zip");
            inputBoundary.DidNotReceive().ExecuteAsync(new InstallPackageRequest("", "C:\\setup.exe", "setup.zip"));
        }
    }
}
