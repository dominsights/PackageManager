using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.UninstallPackage
{
    public class UninstallPackageResponse
    {
        public UninstallPackageResponse(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UninstallPackageResponse response &&
                   Message == response.Message;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Message);
        }
    }
}
