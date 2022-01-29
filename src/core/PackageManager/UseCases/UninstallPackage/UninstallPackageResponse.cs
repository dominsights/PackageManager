using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.UninstallPackage
{
    public class UninstallPackageResponse
    {
        private string packageName;

        public UninstallPackageResponse(string packageName)
        {
            this.packageName = packageName;
        }

        public override bool Equals(object? obj)
        {
            return obj is UninstallPackageResponse response &&
                   packageName == response.packageName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(packageName);
        }
    }
}
