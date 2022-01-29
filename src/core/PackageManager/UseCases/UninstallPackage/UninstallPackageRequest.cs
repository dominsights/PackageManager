using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.UninstallPackage
{
    public class UninstallPackageRequest
    {
        public UninstallPackageRequest(string packageName)
        {
            this.PackageName = packageName;
        }

        public string PackageName { get; set; }
    }
}
