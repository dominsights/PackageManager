using DgSystems.PackageManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public class InstallPackageInteractor : InstallPackageInputBoundary
    {
        private InstallPackageOutputBoundary presenter;

        public InstallPackageInteractor(InstallPackageOutputBoundary presenter)
        {
            this.presenter = presenter;
        }

        public void Execute(InstallPackageRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
