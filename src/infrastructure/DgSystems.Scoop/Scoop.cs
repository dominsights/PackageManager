using DgSystems.PackageManager.Install;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class Scoop : PackageManager.Install.PackageManager
    {
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
