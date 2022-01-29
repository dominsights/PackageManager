using DgSystems.PackageManager.Entities;

namespace DgSystems.PackageManager.UseCases.UninstallPackage
{
    public class UninstallPackageInteractor
    {
        private UninstallPackageOutputBoundary uninstallPresenter;
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
                var uninstallResult = await uninstaller.Uninstall(request.PackageName);

                if (uninstallResult == UninstallationStatus.Success)
                {
                    uninstallPresenter.PresentAsync(new UninstallPackageResponse($"{request.PackageName} was uninstalled successfully."));
                }
                else
                {
                    uninstallPresenter.PresentAsync(new UninstallPackageResponse($"{request.PackageName} failed to uninstall."));
                }
            }
            catch (Exception ex)
            {
                uninstallPresenter.PresentAsync(new UninstallPackageResponse($"{request.PackageName} failed to uninstall."));
            }
        }
    }
}
