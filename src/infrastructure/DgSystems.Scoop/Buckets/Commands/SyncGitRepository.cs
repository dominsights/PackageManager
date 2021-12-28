namespace DgSystems.Scoop.Buckets.Commands
{
    internal class SyncGitRepository : Command
    {
        private string rootFolder;
        private readonly CommandLineShell console;

        public SyncGitRepository(string rootFolder, CommandLineShell console)
        {
            this.rootFolder = rootFolder;
            this.console = console;
        }

        public async Task Execute()
        {
            await console.Execute(new List<string>
            {
                $"cd {rootFolder}/manifests",
                "git add .",
                "git commit -m \"Sync\"",
                "scoop update"
            });
        }

        public Task Undo()
        {
            throw new NotImplementedException();
        }
    }
}
