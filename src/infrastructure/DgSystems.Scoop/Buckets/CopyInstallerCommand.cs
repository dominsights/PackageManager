using System.IO.Abstractions;

namespace DgSystems.Scoop
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

        public void Execute()
        {
            file.Copy(v1, v2);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
