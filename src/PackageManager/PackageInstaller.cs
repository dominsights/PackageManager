using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DgSystems.PackageManagerUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DgSystems.PackageManager
{
    internal class PackageInstaller
    {
        private readonly PackageManager packageManager;

        public PackageInstaller(PackageManager packageManager)
        {
            this.packageManager = packageManager;
        }

        internal InstallationStatus Install(Package program)
        {
            throw new NotImplementedException();
        }
    }
}
