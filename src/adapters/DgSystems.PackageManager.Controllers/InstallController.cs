using DgSystems.PackageManager.UseCases.InstallPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Controllers
{
    public class InstallController
    {
        private readonly InstallPackageInputBoundary installPackageInteractor;

        public InstallController(InstallPackageInputBoundary installPackageInteractor)
        {
            this.installPackageInteractor = installPackageInteractor;
        }
    }
}
