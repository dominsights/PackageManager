using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    internal class Response
    {
        private string v1;
        private string v2;

        public Response(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}
