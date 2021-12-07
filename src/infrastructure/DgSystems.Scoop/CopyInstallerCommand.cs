using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class CopyInstallerCommand : Command
    {
        private string v1;
        private string v2;

        public CopyInstallerCommand(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
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
