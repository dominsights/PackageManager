using System.IO.Abstractions;

namespace DgSystems.Scoop
{
    internal class CopyManifestCommand : Command
    {
        private IFile file;
        private string v1;
        private string v2;

        public CopyManifestCommand(IFile file, string v1, string v2)
        {
            this.file = file;
            this.v1 = v1;
            this.v2 = v2;
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
