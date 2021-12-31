using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class ExtractPackage : Command
    {
        private readonly string sourceArchiveFileName;
        private readonly string destinationDirectoryName;
        private readonly ExtractToDirectory extract;
        private readonly IFileSystem fileSystem;

        public ExtractPackage(string sourceArchiveFileName, string destinationDirectoryName, ExtractToDirectory extract, System.IO.Abstractions.IFileSystem fileSystem)
        {
            this.sourceArchiveFileName = sourceArchiveFileName;
            this.destinationDirectoryName = destinationDirectoryName;
            this.extract = extract;
            this.fileSystem = fileSystem;
        }

        public Task Execute()
        {
            return Task.Run(() =>
            {
                extract(sourceArchiveFileName, destinationDirectoryName);
            });
        }

        public Task Undo()
        {
            string fileName = fileSystem.Path.GetFileNameWithoutExtension(sourceArchiveFileName);
            string directory = fileSystem.Path.Combine(destinationDirectoryName, fileName);
            fileSystem.Directory.Delete(directory, true);
            return Task.CompletedTask;
        }
    }
}
