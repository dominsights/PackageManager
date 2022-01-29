using DgSystems.PackageManager.UseCases.UninstallPackage;

namespace DgSystems.PackageManager.Controllers.UninstallPackage
{
    public class UninstallController
    {
        public UninstallController(UninstallPackageInteractor uninstallInteractor)
        {
            this.uninstallInteractor = uninstallInteractor;
        }

        private UninstallPackageInteractor uninstallInteractor;

        public async Task UninstallAsync(string packageName)
        {
            await uninstallInteractor.ExecuteAsync(new UninstallPackageRequest(packageName));
        }
    }
}