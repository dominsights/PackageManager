using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class SyncGitRepositoryCommand : Command
    {
        private string rootFolder;
        private readonly CommandLineShell console;

        public SyncGitRepositoryCommand(string rootFolder, CommandLineShell console)
        {
            this.rootFolder = rootFolder;
            this.console = console;
        }

        public async Task Execute()
        {
            await console.Execute(new List<string>
            {
                $"Set-Location {rootFolder}/manifests",
                "git add .",
                "git commit -m \"Sync\""
            });
        }

        public Task Undo()
        {
            throw new NotImplementedException();
        }
    }
}
