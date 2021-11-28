using DgSystems.PackageManager.Setup.Events;

namespace DgSystems.PackageManager.Setup
{
    internal class PackageWithDependenciesStrategy : InstallationStrategy
    {
        public async Task Install(Guid installationId, Package package, PackageManager packageManager, Notifier notifier)
        {
            bool areDependenciesInstalled = true;

            foreach (var dependency in package.Dependencies)
            {
                if (!packageManager.IsPackageValid(dependency))
                    return;

                var dependencyResult = await packageManager.InstallAsync(dependency);

                if (dependencyResult == InstallationStatus.Success)
                {
                    notifier.Notify(new InstallationExecuted(installationId, dependency.Name));
                }
                else
                {
                    notifier.Notify(new InstallationFailed(installationId, dependency.Name));
                    areDependenciesInstalled = false;
                    break;
                }
            }

            if (areDependenciesInstalled)
            {
                if (!packageManager.IsPackageValid(package))
                {
                    notifier.Notify(new InstallationRejected(installationId, "Package is invalid."));
                    return;
                }

                var installationResult = await packageManager.InstallAsync(package);

                if (installationResult == InstallationStatus.Success)
                    notifier.Notify(new InstallationExecuted(installationId, package.Name));
            }
            else
            {
                notifier.Notify(new InstallationFailed(installationId, package.Name));
            }
        }
    }
}
