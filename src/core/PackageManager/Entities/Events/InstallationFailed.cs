namespace DgSystems.PackageManager.Entities.Events
{
    public record InstallationFailed(Guid id, string name, string reason);
}