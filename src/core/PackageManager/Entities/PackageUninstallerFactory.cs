using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager
{
    public interface PackageUninstallerFactory
    {
        public Entities.PackageUninstallation Create();
    }
}
