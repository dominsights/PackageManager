namespace DgSystems.Scoop
{
    internal interface CommandLineShell
    {
        Task Execute(string command);
        /// <summary>
        /// Execute commands in one batch.
        /// </summary>
        /// <param name="list"></param>
        Task Execute(List<string> commands);
    }
}