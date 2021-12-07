using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class ExtractPackageCommand : Command
    {
        private string sourceArchiveFileName;
        private string destinationDirectoryName;

        public ExtractPackageCommand(string sourceArchiveFileName, string destinationDirectoryName)
        {
            this.sourceArchiveFileName = sourceArchiveFileName;
            this.destinationDirectoryName = destinationDirectoryName;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
