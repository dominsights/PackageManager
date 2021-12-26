using DgSystems.PackageManager.Entities;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    internal class Interactor : InputBoundary
    {
        private readonly OutputBoundary presenter;
        private readonly Entities.PackageManager packageManager;
        private readonly Notifier notifier;

        public Interactor(OutputBoundary presenter, Entities.PackageManager packageManager, Notifier notifier)
        {
            this.presenter = presenter;
            this.packageManager = packageManager;
            this.notifier = notifier;
        }

        public async Task ExecuteAsync(Request request)
        {
            var installation = new Installation(packageManager, notifier);
            var installationStatus = await installation.Install(new Package(request.Name, request.Path, request.FileName));
            if (installationStatus == InstallationStatus.Success)
            {
                presenter.Execute(new Response(request.Name, $"{request.Name} was installed successfully."));
            }
            else
            {
                presenter.Execute(new Response(request.Name, $"{request.Name} failed to install."));
            }
        }
    }
}
