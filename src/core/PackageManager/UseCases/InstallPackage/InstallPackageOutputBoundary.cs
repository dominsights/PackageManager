﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public interface InstallPackageOutputBoundary
    {
        void PresentAsync(InstallPackageResponse installPackageResponse);
    }
}
