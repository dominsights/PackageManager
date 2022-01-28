using DgSystems.PackageManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.UninstallPackage
{
    public class UninstallPackageInteractor
    {
        private UninstallPackageOutputBoundary uninstallPresenter;

        public UninstallPackageInteractor(UninstallPackageOutputBoundary uninstallPresenter, PackageUninstallation uninstaller)
        {
            this.uninstallPresenter = uninstallPresenter;
        }

        public async Task ExecuteAsync(UninstallPackageRequest request)
        {

        }
    }
}
