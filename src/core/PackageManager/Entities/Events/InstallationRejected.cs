namespace DgSystems.PackageManager.Entities.Events
{
    public record InstallationRejected(Guid installatonId, string reason);
}