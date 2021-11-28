namespace DgSystems.PackageManager.Install.Events
{
    public record InstallationRejected(Guid installatonId, string reason);
}