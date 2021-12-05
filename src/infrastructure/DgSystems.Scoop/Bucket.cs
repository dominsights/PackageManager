using DgSystems.PackageManager.Install;
using System.IO.Abstractions;
using System.IO.Compression;

namespace DgSystems.Scoop
{
    internal class Bucket
    {
        private string name;
        private readonly string folder;
        private CommandLineShell console;

        public Bucket(string name, string folder, CommandLineShell console, IFile file, Downloader downloader)
        {
            this.console = console;
            this.name = name;
            this.folder = folder;
        }

        internal void Sync(Package package)
        {
            throw new NotImplementedException();
        }

        protected virtual void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
        }
    }
}
