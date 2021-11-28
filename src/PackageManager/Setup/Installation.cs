using DgSystems.PackageManager.Setup.Events;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DgSystems.PackageManagerUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DgSystems.PackageManager.Setup
{
    internal class Installation
    {
        private readonly PackageManager packageManager;
        private readonly Notifier notifier;
        private readonly InstallationStrategy installationStrategy;

        public Installation(PackageManager packageManager, Notifier notifier, InstallationStrategy installationStrategy)
        {
            this.packageManager = packageManager;
            this.notifier = notifier;
            Id = Guid.NewGuid();
            this.installationStrategy = installationStrategy;
        }

        public Guid Id { get; internal set; }

        /// <summary>
        /// Execute installation commands.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        internal async Task Install(Package package)
        {
            if (package is null)
            {
                notifier.Notify(new InstallationRejected(Id, "Package is null."));
                return;
            }

            await installationStrategy.Install(Id, package, packageManager, notifier);
        }
    }
}
