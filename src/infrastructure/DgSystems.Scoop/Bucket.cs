using DgSystems.PackageManager.Install;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Linq;
using System.Net;
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

        public virtual void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
        }

        public virtual void DownloadFile(Uri address, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
