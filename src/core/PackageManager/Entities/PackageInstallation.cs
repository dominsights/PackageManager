﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Entities
{
    public interface PackageInstallation
    {
        Task<InstallationStatus> Install(Package package);
        bool IsPackageValid(Package package);
    }
}
