using DgSystems.PackageManager.Entities;

namespace DgSystems.PackageManager.UseCases.UninstallPackage
{
    public class UninstallPackageInteractor
    {
        private readonly UninstallPackageOutputBoundary uninstallPresenter;
        private readonly PackageUninstallation uninstaller;

        public UninstallPackageInteractor(UninstallPackageOutputBoundary uninstallPresenter, PackageUninstallation uninstaller)
        {
            this.uninstallPresenter = uninstallPresenter;
            this.uninstaller = uninstaller;
        }

        public async Task ExecuteAsync(UninstallPackageRequest request)
        {
            try
            {
                UninstallationStatus uninstallResult = await uninstaller.Uninstall(request.PackageName);
                switch (uninstallResult)
                {
                    case UninstallationStatus.Success:
                        string sucessMessage = $"{request.PackageName} was uninstalled successfully.";
                        uninstallPresenter.PresentAsync(new UninstallPackageResponse(sucessMessage));
                        break;

                    default:
                        PresentError(request.PackageName);
                        break;
                }
            }
            catch
            {
                PresentError(request.PackageName);
            }
        }

        private void PresentError(string packageName)
        {
            string errorMessage = $"{packageName} failed to uninstall.";
            uninstallPresenter.PresentAsync(new UninstallPackageResponse(errorMessage));
        }
    }
}
