using System.Collections.ObjectModel;
using System.Management.Automation;

namespace DgSystems.PowerShell
{
    internal interface PowerShellCLI
    {
        public System.Management.Automation.PowerShell AddScript(string command);
        public Collection<PSObject> Invoke();
    }
}
