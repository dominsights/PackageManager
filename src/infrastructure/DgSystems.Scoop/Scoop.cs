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

        public Scoop(CommandLineShell console, BucketList bucketList)
        {
            this.console = console;
        }

        // create bucket if it doesn't exist yet
        // download package from path provided
        // update bucket with new package
        // install package






        public Task<InstallationStatus> InstallAsync(Package package)
        {
            throw new NotImplementedException();
        }

        public bool IsPackageValid(Package package)
        {
            throw new NotImplementedException();
        }
    }
}
