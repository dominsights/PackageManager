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
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "powershell.exe";
            startInfo.Arguments = command;
            startInfo.RedirectStandardError = true;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0) throw new Exception("Operation not executed!");
            process.Close();

            return Task.CompletedTask;
        }

        public Task Execute(List<string> commands)
        {
            string command = string.Join("; ", commands);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "powershell.exe";
            startInfo.Arguments = command;
            startInfo.RedirectStandardError = true;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            System.Diagnostics.Debug.WriteLine(process.StandardError.ReadLine());

            if (process.ExitCode != 0) throw new Exception("Operation not executed!");
            process.Close();

            return Task.CompletedTask;
        }
    }
}
