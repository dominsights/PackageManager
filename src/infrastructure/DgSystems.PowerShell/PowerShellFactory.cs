using DgSystems.Scoop;

namespace DgSystems.PowerShell
{
    public class PowerShellFactory : CommandLineShellFactory
    {
        private readonly Process process;

        public PowerShellFactory(Process process)
        {
            this.process = process;
        }


        public CommandLineShell Create()
        {
            return new PowerShell(process);
        }
    }
}
