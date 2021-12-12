using DgSystems.PackageManager.Install;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DgSystems.ScoopUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DgSystems.Scoop
{
    internal class Scoop : PackageManager.Install.PackageManager
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

        // create bucket if it doesn't exist yet
        // download package from path provided
        // update bucket with new package
        // install package






        public Task<InstallationStatus> Install(Package package)
        {
            var bucket = bucketList.Default();
            bucket.Sync(package, downloadFolder, extractToDirectory);
            return Task.FromResult(InstallationStatus.Failure);
        }

        public bool IsPackageValid(Package package)
        {
            throw new NotImplementedException();
        }
    }
}
