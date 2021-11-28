﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Setup
{
    internal interface PackageManager
    {
        Task<InstallationStatus> InstallAsync(Package package);
        bool IsPackageValid(Package package);
    }
}