namespace DgSystems.PackageManager.Install.Events
{
    public record InstallationExecuted(Guid InstallationId, string PackageName);
}