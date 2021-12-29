using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class CopyInstaller : Command
    {
        private string source;
        private string destination;
        private readonly IFileSystem fileSystem;

        public CopyInstaller(string source, string destination, IFileSystem fileSystem)
        {
            this.source = source;
            this.destination = destination;
            this.fileSystem = fileSystem;
        }

        public Task Execute()
        {
            return Task.Run(() =>
            {
                if (fileSystem.File.Exists(destination))
                {
                    string fileName = fileSystem.Path.GetFileNameWithoutExtension(destination);
                    var directory = fileSystem.Path.GetDirectoryName(destination);
                    string extension = fileSystem.Path.GetExtension(destination);

                    string destFileName = fileSystem.Path.Combine(directory, fileName + $"_backup{extension}");
                    fileSystem.File.Copy(destination, destFileName, true);
                }

                if (!fileSystem.Directory.Exists(fileSystem.Path.GetDirectoryName(destination)))
                {
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
