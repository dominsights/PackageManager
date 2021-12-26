using DgSystems.PackageManager.Entities;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    internal class Interactor : InputBoundary
    {
        private OutputBoundary presenter;
        private readonly Entities.PackageManager packageManager;
        private readonly Notifier notifier;

        public Interactor(OutputBoundary presenter, Entities.PackageManager packageManager, Notifier notifier)
        {
            this.presenter = presenter;
            this.packageManager = packageManager;
            this.notifier = notifier;
        }

        public void Execute(Request request)
        {
            var installation = new Installation(packageManager, notifier);
            throw new NotImplementedException();
        }
    }
}
