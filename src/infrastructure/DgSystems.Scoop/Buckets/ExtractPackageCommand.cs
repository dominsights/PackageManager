using System.IO.Compression;

namespace DgSystems.Scoop
{
    internal class ExtractPackageCommand : Command
    {
        private string sourceArchiveFileName;
        private string destinationDirectoryName;
        private readonly ExtractToDirectory extract;

        public ExtractPackageCommand(string sourceArchiveFileName, string destinationDirectoryName, ExtractToDirectory extract)
        {
            this.sourceArchiveFileName = sourceArchiveFileName;
            this.destinationDirectoryName = destinationDirectoryName;
            this.extract = extract;
        }

        public void Execute()
        {
            extract(sourceArchiveFileName, destinationDirectoryName);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
