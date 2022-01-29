using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Presenters.UninstallPackage
{
    public class UninstallPackageOutput
    {
        public UninstallPackageOutput(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
