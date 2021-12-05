using DgSystems.PackageManager.Install;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class Bucket
    {
        private string name;
        private readonly string folder;
        private CommandLineShell console;

        public Bucket(string name, string folder, CommandLineShell console, IFile file)
        {
            this.console = console;
            this.name = name;
            this.folder = folder;
        }

        internal void Sync(Package package)
        {
            throw new NotImplementedException();
        }
    }
}
