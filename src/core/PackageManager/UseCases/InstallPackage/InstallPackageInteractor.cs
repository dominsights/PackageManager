using DgSystems.PackageManager.Entities;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public class InstallPackageInteractor : InstallPackageInputBoundary
    {
        private readonly InstallPackageOutputBoundary presenter;
        private readonly Entities.PackageManager packageManager;
        private readonly Notifier notifier;

        public InstallPackageInteractor(InstallPackageOutputBoundary presenter, Entities.PackageManager packageManager, Notifier notifier)
        {
            this.presenter = presenter;
            this.packageManager = packageManager;
            this.notifier = notifier;
        }

        public async Task ExecuteAsync(InstallPackageRequest request)
        {
            var installation = new Installation(packageManager, notifier);
            var installationStatus = await installation.Install(new Package(request.Name, request.Path, request.FileName));
            if (installationStatus == InstallationStatus.Success)
            {
                presenter.PresentAsync(new InstallPackageResponse(request.Name, $"{request.Name} was installed successfully."));
            }
            else
            {
                presenter.PresentAsync(new InstallPackageResponse(request.Name, $"{request.Name} failed to install."));
            }
        }
    }
}
