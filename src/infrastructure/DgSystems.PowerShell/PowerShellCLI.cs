using System.Collections.ObjectModel;
using System.Management.Automation;

namespace DgSystems.PowerShell
{
    internal interface PowerShellCLI
    {
        public System.Management.Automation.PowerShell AddCommand(string command);
        public Collection<PSObject> Invoke();
    }
}
