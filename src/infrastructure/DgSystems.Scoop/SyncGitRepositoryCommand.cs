using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class SyncGitRepositoryCommand : Command
    {
        private string rootFolder;

        public SyncGitRepositoryCommand(string rootFolder, CommandLineShell console)
        {
            this.rootFolder = rootFolder;
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
