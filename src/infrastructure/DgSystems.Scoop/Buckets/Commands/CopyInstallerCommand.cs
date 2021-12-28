using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class CopyInstallerCommand : Command
    {
        private string v1;
        private string v2;
        private readonly IFile file;

        public CopyInstallerCommand(string sourceFileName, string destFileName, IFile file)
        {
            this.v1 = sourceFileName;
            this.v2 = destFileName;
            this.file = file;
        }

        public Task Execute()
        {
            return Task.Run(() =>
            {
                file.Copy(v1, v2, true);
            });
        }

        public Task Undo()
        {
            throw new NotImplementedException();
        }
    }
}
