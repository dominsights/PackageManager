namespace DgSystems.PackageManager.Setup.Events
{
    public class InstallationFailed
    {
        private Guid id;
        private string name;

        public InstallationFailed(Guid id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}