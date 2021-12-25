using DgSystems.Scoop;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DgSystems.PowerShellUnitTests")]
[assembly: InternalsVisibleTo("DgSystems.ScoopUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DgSystems.PowerShell
{
    internal class PowerShell : CommandLineShell
    {
        private PowerShellCLI powershellCLI;

        public PowerShell(PowerShellCLI powershellCLI)
        {
            this.powershellCLI = powershellCLI;
        }

        public Task Execute(string command)
        {
            powershellCLI.AddCommand(command);
            powershellCLI.Invoke();
            return Task.CompletedTask;
        }

        public Task Execute(List<string> commands)
        {
            foreach (string command in commands)
            {
                powershellCLI.AddCommand(command);
            }
            powershellCLI.Invoke();
            return Task.CompletedTask;
        }
    }
}
