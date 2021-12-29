using DgSystems.Scoop;

namespace DgSystems.PowerShell
{
    public class PowerShellFactory : CommandLineShellFactory
    {
        public CommandLineShell Create()
        {
            return new PowerShell(new Process());
        }
    }
}
