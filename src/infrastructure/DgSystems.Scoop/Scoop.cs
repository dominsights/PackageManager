using DgSystems.PackageManager.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DgSystems.ScoopUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DgSystems.Scoop
{
    internal class Scoop : PackageManager.Entities.PackageManager
    {
        private CommandLineShell console;
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
                await console.Execute($"scoop install {package.Name}");
                return InstallationStatus.Success;
            }

            return InstallationStatus.Failure;
        }

        public bool IsPackageValid(Package package)
        {
            return true; // TODO: validate manifest
        }
    }
}
