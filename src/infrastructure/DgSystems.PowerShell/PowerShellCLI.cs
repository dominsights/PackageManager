using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PowerShell
{
    internal interface PowerShellCLI
    {
        public void AddCommand(string command);
        public void Invoke();
    }
}
