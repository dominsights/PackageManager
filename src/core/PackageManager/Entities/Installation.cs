﻿using DgSystems.PackageManager.Entities.Events;

namespace DgSystems.PackageManager.Entities
{
    public class Installation
    {
        private readonly PackageInstallation packageManager;
        private readonly Notifier notifier;

        public Installation(PackageInstallation packageManager, Notifier notifier)
        {
            this.packageManager = packageManager;
            this.notifier = notifier;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; internal set; }

        /// <summary>
        /// Execute installation commands.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public async Task<InstallationStatus> Install(Package package)
        {
            if (package is null)
            {
                notifier.Notify(new InstallationRejected(Id, "Package is null."));
                return InstallationStatus.Failure;
            }

            return await Install_InternalAsync(Id, package, packageManager, notifier);
        }

        private async Task<InstallationStatus> Install_InternalAsync(Guid installationId, Package package, PackageInstallation packageManager, Notifier notifier)
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

        private async Task<InstallationStatus> InstallDependencies(Guid installationId, PackageInstallation packageManager, Notifier notifier, IEnumerable<Package> dependencies)
        {
            foreach (var dependency in dependencies)
            {
                var installationStatus = await Install_InternalAsync(installationId, dependency, packageManager, notifier);
                if (installationStatus == InstallationStatus.Failure)
                {
                    return installationStatus;
                }
            }

            return InstallationStatus.Success;
        }

        private static async Task<InstallationStatus> InstallPackage(Guid installationId, Package package, PackageInstallation packageManager, Notifier notifier)
        {
            if (!packageManager.IsPackageValid(package))
            {
                notifier.Notify(new InstallationRejected(installationId, "Package is invalid."));
                return InstallationStatus.Failure;
            }

            var installationResult = await packageManager.Install(package);

            if (installationResult == InstallationStatus.Success)
            {
                notifier.Notify(new InstallationExecuted(installationId, package.Name));
                return InstallationStatus.Success;
            }
            else
            {
                notifier.Notify(new InstallationFailed(installationId, package.Name, $"Installation failed for package {package.Name}"));
                return installationResult;
            }
        }
    }
}
