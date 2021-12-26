using DgSystems.PackageManager.UseCases.InstallPackage;

namespace DgSystems.PackageManager.Controllers.InstallPackage
{
    public class InstallController
    {
        private readonly InputBoundary installPackageInteractor;

        public InstallController(InputBoundary installPackageInteractor)
        {
            this.installPackageInteractor = installPackageInteractor;
        }

        public void Install(string name, string path, string fileName)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(fileName))
                return;
            
            installPackageInteractor.ExecuteAsync(new Request(name, path, fileName));
        }
    }
}
