namespace DgSystems.Scoop
{
    public interface CommandLineShell
    {
        Task Execute(string command);
        /// <summary>
        /// Execute commands in one batch.
        /// </summary>
        /// <param name="list"></param>
        Task Execute(List<string> commands);
    }
}