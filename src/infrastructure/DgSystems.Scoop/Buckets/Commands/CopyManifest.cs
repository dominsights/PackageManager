using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class CopyManifest : Command
    {
        private readonly IFileSystem fileSystem;
        private readonly string source;
        private readonly string destination;
        private readonly CommandLineShell commandLine;

        public CopyManifest(IFileSystem fileSystem, string source, string destination, CommandLineShell commandLine)
        {
            this.fileSystem = fileSystem;
            this.source = source;
            this.destination = destination;
            this.commandLine = commandLine;
        }

        public Task Execute()
        {
            return Task.Run(() =>
            {
                string directory = fileSystem.Path.GetDirectoryName(destination);
                if (!fileSystem.Directory.Exists(directory))
                {
                    fileSystem.Directory.CreateDirectory(directory);
                }

                fileSystem.File.Copy(source, destination, true);
            });
        }

        public async Task Undo()
        {
            await commandLine.Execute(new List<string> {
                "git reset",
                "git checkout .",
                "git clean -fdx"
            });
        }
    }
}
