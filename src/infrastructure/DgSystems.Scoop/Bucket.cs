using DgSystems.PackageManager.Install;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class Bucket
    {
        private string v;

        private CommandLineShell console;

        public Bucket(CommandLineShell console, string v)
        {
            this.console = console;
            this.v = v;
        }

        internal void Sync(Package package)
        {
            throw new NotImplementedException();
        }
    }
}
