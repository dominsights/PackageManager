namespace DgSystems.PackageManager.Entities.Events
{
    public record InstallationExecuted(Guid InstallationId, string PackageName);
}