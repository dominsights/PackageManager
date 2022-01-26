using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.UninstallPackage
{
    public class UninstallPackageRequest
    {
        private string v;

        public UninstallPackageRequest(string v)
        {
            this.v = v;
        }
    }
}
