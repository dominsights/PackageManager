using DgSystems.PackageManager.Setup.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DgSystems.PackageManagerUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DgSystems.PackageManager.Setup
{
    internal class Installation
    {
        private readonly PackageManager packageManager;
        private readonly Notifier notifier;

        public Installation(PackageManager packageManager, Notifier notifier)
        {
            this.packageManager = packageManager;
            this.notifier = notifier;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; internal set; }

        internal async Task Install(Package package)
        {
            if (package is null)
            {
                notifier.Notify(new InstallationRejected(Id, "Package is null."));
                return;
            }

            bool areDependenciesInstalled = true;

            if (package.Dependencies != null && package.Dependencies.Any())
            {
                foreach (var dependency in package.Dependencies)
                {
                    if (!packageManager.IsPackageValid(dependency))
                        return;

                    var dependencyResult = await packageManager.InstallAsync(dependency);

                    if (dependencyResult == InstallationStatus.Success)
                    {
                        notifier.Notify(new InstallationExecuted(Id, dependency.Name));
                    }
                    else
                    {
                        notifier.Notify(new InstallationFailed(Id, dependency.Name));
                        areDependenciesInstalled = false;
                        break;
                    }
                }
            }

            if (areDependenciesInstalled)
            {
                if (!packageManager.IsPackageValid(package))
                {
                    notifier.Notify(new InstallationRejected(Id, "Package is invalid."));
                    return;
                }

                var installationResult = await packageManager.InstallAsync(package);

                if (installationResult == InstallationStatus.Success)
                    notifier.Notify(new InstallationExecuted(Id, package.Name));
                else
                    notifier.Notify(new InstallationFailed(Id, package.Name));
            }
        }
    }
}
