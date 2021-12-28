using System.Collections.ObjectModel;
using System.Management.Automation;

namespace DgSystems.PowerShell
{
    internal class PowerShellWrapper : PowerShellCLI
    {
        private System.Management.Automation.PowerShell powerShell;

        public PowerShellWrapper()
        {
            powerShell = System.Management.Automation.PowerShell.Create();
        }

        public System.Management.Automation.PowerShell AddScript(string command)
        {
            return powerShell.AddScript(command);
        }

        public Collection<PSObject> Invoke()
        {
            Collection<PSObject> pSObjects = powerShell.Invoke();
            return pSObjects;
        }
    }
}
