namespace DgSystems.PackageManager.Entities
{
    public interface PackageUninstallation
    {
        public Task<UninstallationStatus> Uninstall(string packageName);
    }
}
