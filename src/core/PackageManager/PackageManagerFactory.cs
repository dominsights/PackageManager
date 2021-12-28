using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager
{
    public interface PackageManagerFactory
    {
        public Entities.PackageManager Create();
    }
}
