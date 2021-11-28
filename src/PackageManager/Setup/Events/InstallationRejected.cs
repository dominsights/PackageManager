namespace DgSystems.PackageManager.Setup.Events
{
    public record InstallationRejected(Guid installatonId, string reason);
}