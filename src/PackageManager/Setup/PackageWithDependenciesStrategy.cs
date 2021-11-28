using DgSystems.PackageManager.Setup.Events;

namespace DgSystems.PackageManager.Setup
{
    internal class PackageWithDependenciesStrategy : InstallationStrategy
    {
        public async Task Install(Guid installationId, Package package, PackageManager packageManager, Notifier notifier)
        {
            await Install_InternalAsync(installationId, package, packageManager, notifier);
        }

        private async Task<InstallationStatus> Install_InternalAsync(Guid installationId, Package package, PackageManager packageManager, Notifier notifier)
        {
            if (package.Dependencies == null || !package.Dependencies.Any())
            {
                return await InstallPackage(installationId, package, packageManager, notifier);
            }

            var dependenciesInstallationStatus = await InstallDependencies(installationId, packageManager, notifier, package.Dependencies);

            if (dependenciesInstallationStatus == InstallationStatus.Success)
            {
                return await InstallPackage(installationId, package, packageManager, notifier);
            }

            notifier.Notify(new InstallationFailed(installationId, package.Name, "Dependency not installed."));
            return dependenciesInstallationStatus;
        }

        private async Task<InstallationStatus> InstallDependencies(Guid installationId, PackageManager packageManager, Notifier notifier, IEnumerable<Package> dependencies)
        {
            foreach(var dependency in dependencies)
            {
                var installationStatus = await Install_InternalAsync(installationId, dependency, packageManager, notifier);
                if(installationStatus == InstallationStatus.Failure)
                {
                    return installationStatus;
                }
            }

            return InstallationStatus.Success;
        }

        private static async Task<InstallationStatus> InstallPackage(Guid installationId, Package package, PackageManager packageManager, Notifier notifier)
        {
            if (!packageManager.IsPackageValid(package))
            {
                notifier.Notify(new InstallationRejected(installationId, "Package is invalid."));
                return InstallationStatus.Failure;
            }

            var installationResult = await packageManager.InstallAsync(package);

            if (installationResult == InstallationStatus.Success)
            {
                notifier.Notify(new InstallationExecuted(installationId, package.Name));
                return InstallationStatus.Success;
            }
            else
            {
                notifier.Notify(new InstallationFailed(installationId, package.Name, $"Installation failed for package {package.Name}"));
                return InstallationStatus.Failure;
            }
        }
    }
}
