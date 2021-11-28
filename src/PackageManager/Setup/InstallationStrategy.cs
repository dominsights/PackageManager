using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Setup
{
    internal interface InstallationStrategy
    {
        Task Install(Guid installationId, Package package, PackageManager packageManager, Notifier notifier);
    }
}
