using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public class InstallPackageResponse
    {
        private string packageName;
        private string message;

        public string PackageName { get => packageName; set => packageName = value; }
        public string Message { get => message; set => message = value; }

        public InstallPackageResponse(string packageName, string message)
        {
            this.PackageName = packageName;
            this.Message = message;
        }

        public override bool Equals(object? obj)
        {
            return obj is InstallPackageResponse response &&
                   PackageName == response.PackageName &&
                   Message == response.Message;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PackageName, Message);
        }
    }
}
