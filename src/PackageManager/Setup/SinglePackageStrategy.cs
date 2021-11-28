using DgSystems.PackageManager.Setup.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Setup
{
    internal class SinglePackageStrategy : InstallationStrategy
    {
        public async Task Install(Guid installationId, Package package, PackageManager packageManager, Notifier notifier)
        {
            if (!packageManager.IsPackageValid(package))
            {
                notifier.Notify(new InstallationRejected(installationId, "Package is invalid."));
                return;
            }

            var installationResult = await packageManager.InstallAsync(package);

            if (installationResult == InstallationStatus.Success)
                notifier.Notify(new InstallationExecuted(installationId, package.Name));
            else
                notifier.Notify(new InstallationFailed(installationId, package.Name, $"Installation failed for package {package.Name}"));
        }
    }
}
