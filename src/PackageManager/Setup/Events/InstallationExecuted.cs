namespace DgSystems.PackageManager.Setup.Events
{
    public record InstallationExecuted(Guid InstallationId, string PackageName);
}