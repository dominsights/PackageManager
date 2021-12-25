using DgSystems.PackageManager.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Controllers
{
    public class InstallController
    {
        private readonly InstallPackageInteractor installPackageInteractor;

        public InstallController(InstallPackageInteractor installPackageInteractor)
        {
            this.installPackageInteractor = installPackageInteractor;
        }
    }
}
