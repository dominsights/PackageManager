using DgSystems.PackageManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public class InstallPackageRequest
    {
        private Package package;

        public InstallPackageRequest(Package package)
        {
            this.package = package;
        }
    }
}
