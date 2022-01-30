using DgSystems.PackageManager.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DgSystems.ScoopUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DgSystems.Scoop
{
    public class Scoop : PackageInstallation, PackageUninstallation
    {
        private readonly CommandLineShell console;
        private readonly BucketList bucketList;
        private readonly string downloadFolder;
        private readonly ExtractToDirectory extractToDirectory;

        public Scoop(CommandLineShell console, BucketList bucketList, string downloadFolder, ExtractToDirectory extractToDirectory)
        {
            this.console = console;
            this.bucketList = bucketList;
            this.downloadFolder = downloadFolder;
            this.extractToDirectory = extractToDirectory;
        }

        public async Task<InstallationStatus> Install(Package package)
        {
            var bucket = bucketList.Default();
            bool success = await bucket.Sync(package, downloadFolder, extractToDirectory);

            if (success)
            {
                try
                {
                    await console.Execute($"scoop install {package.Name}");
                    return InstallationStatus.Success;
                }
                catch
                {
                    return InstallationStatus.Failure;
                }
            }

            return InstallationStatus.Failure;
        }

        public bool IsPackageValid(Package package)
        {
            return true; // TODO: validate manifest
        }

        public async Task<UninstallationStatus> Uninstall(string packageName)
        {
            try
            {
                await console.Execute($"scoop uninstall {packageName}");
                return UninstallationStatus.Success;
            }
            catch (Exception e)
            {
                return UninstallationStatus.Failure;
            }
        }
    }
}
