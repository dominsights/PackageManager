using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public class Response
    {
        private string packageName;
        private string message;

        public Response(string packageName, string message)
        {
            this.packageName = packageName;
            this.message = message;
        }

        public override bool Equals(object? obj)
        {
            return obj is Response response &&
                   packageName == response.packageName &&
                   message == response.message;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(packageName, message);
        }
    }
}
