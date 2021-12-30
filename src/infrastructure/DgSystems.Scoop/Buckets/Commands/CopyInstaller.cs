using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class CopyInstaller : Command
    {
        private readonly string source;
        private readonly string destination;
        private readonly IFileSystem fileSystem;
        private readonly string backupFileName;
        private readonly string directory;

        public CopyInstaller(string source, string destination, IFileSystem fileSystem)
        {
            this.source = source;
            this.destination = destination;
            this.fileSystem = fileSystem;

            directory = fileSystem.Path.GetDirectoryName(destination);
            string fileName = fileSystem.Path.GetFileNameWithoutExtension(destination);
            string extension = fileSystem.Path.GetExtension(destination);
            backupFileName = fileSystem.Path.Combine(directory, fileName + $"_backup{extension}");

        }

        public Task Execute()
        {
            return Task.Run(() =>
            {
                if (fileSystem.File.Exists(destination))
                {
                    fileSystem.File.Copy(destination, backupFileName, true);
                }

                if (!fileSystem.Directory.Exists(directory))
                {
                    fileSystem.Directory.CreateDirectory(directory);
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
