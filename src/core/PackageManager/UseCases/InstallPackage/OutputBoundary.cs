﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public interface OutputBoundary
    {
        void PresentAsync(Response installPackageResponse);
    }
}