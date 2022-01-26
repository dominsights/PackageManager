using DgSystems.PackageManager.UseCases.InstallPackage;

namespace DgSystems.PackageManager.Controllers.InstallPackage
{
    public class InstallController
    {
        private readonly InstallPackageInputBoundary installPackageInteractor;

        public InstallController(InstallPackageInputBoundary installPackageInteractor)
        {
            this.installPackageInteractor = installPackageInteractor;
        }

        public void Install(string name, string path, string fileName)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(fileName))
                return;
            
            installPackageInteractor.ExecuteAsync(new InstallPackageRequest(name, path, fileName));
        }
    }
}
