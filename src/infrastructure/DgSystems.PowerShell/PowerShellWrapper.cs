namespace DgSystems.PowerShell
{
    internal class PowerShellWrapper : PowerShellCLI
    {
        private System.Management.Automation.PowerShell powerShell;

        public PowerShellWrapper()
        {
            powerShell = System.Management.Automation.PowerShell.Create();
        }

        public void AddCommand(string command)
        {
            powerShell.AddCommand(command);
        }

        public void Invoke()
        {
            powerShell.Invoke();
        }
    }
}
