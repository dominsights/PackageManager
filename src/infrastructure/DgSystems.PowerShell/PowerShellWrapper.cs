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

        public System.Management.Automation.PowerShell AddCommand(string command)
        {
            return powerShell.AddCommand(command);
        }

        public Collection<PSObject> Invoke()
        {
            return powerShell.Invoke();
        }
    }
}
