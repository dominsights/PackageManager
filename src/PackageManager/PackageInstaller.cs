using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager
{
    internal class PackageInstaller
    {
        private PackageManager @object;

        public PackageInstaller(PackageManager @object)
        {
            this.@object = @object;
        }

        internal object Install(Package program)
        {
            throw new NotImplementedException();
        }
    }
}
