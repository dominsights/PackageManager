using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class CopyManifest : Command
    {
        private IFileSystem fileSystem;
        private string source;
        private string destination;

        public CopyManifest(IFileSystem fileSystem, string source, string destination)
        {
            this.fileSystem = fileSystem;
            this.source = source;
            this.destination = destination;
        }

        public Task Execute()
        {
            return Task.Run(() =>
            {
                if(!fileSystem.Directory.Exists(destination)) {
                    fileSystem.Directory.CreateDirectory(destination);
                }

                fileSystem.File.Copy(source, destination, true);
            });
        }

        public Task Undo()
        {
            throw new NotImplementedException();
        }
    }
}
