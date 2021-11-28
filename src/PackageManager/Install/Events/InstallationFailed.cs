namespace DgSystems.PackageManager.Install.Events
{
    public record InstallationFailed(Guid id, string name, string reason);
}