namespace DgSystems.PackageManager
{
    public record InstallationRejected(Guid installatonId, string reason);
}