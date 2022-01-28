using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.UninstallPackage
{
    public interface UninstallPackageOutputBoundary
    {
        void PresentAsync(UninstallPackageResponse uninstallPackageResponse);
    }
}
