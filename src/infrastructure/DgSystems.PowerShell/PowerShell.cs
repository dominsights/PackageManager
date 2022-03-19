using DgSystems.Scoop;

namespace DgSystems.PowerShell
{
    internal class PowerShell : CommandLineShell
    {
        private readonly Process process;

        public PowerShell(Process process)
        {
            this.process = process;
        }

        public Task Execute(string command)
        {
            int exitCode = process.Execute("powershell.exe", command);
            if (exitCode != 0) 
                throw new Exception("Operation not executed!");
            return Task.CompletedTask;
        }

        public Task Execute(List<string> commands)
        {
            string command = string.Join("; ", commands);

            int exitCode = process.Execute("powershell.exe", command);
            if (exitCode != 0)
                throw new Exception("Operation not executed!");
            return Task.CompletedTask;
        }
    }
}
