using DgSystems.PackageManager.UseCases.InstallPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Controllers.InstallPackage
{
    public class InstallController
    {
        private readonly InputBoundary installPackageInteractor;

        public InstallController(InputBoundary installPackageInteractor)
        {
            this.installPackageInteractor = installPackageInteractor;
        }
    }
}
