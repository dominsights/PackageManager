namespace DgSystems.Scoop
{
    internal interface CommandLineShell
    {
        void Execute(string command);
        /// <summary>
        /// Execute commands in one batch.
        /// </summary>
        /// <param name="list"></param>
        void Execute(List<string> commands);
    }
}