namespace DgSystems.PackageManager
{
    public interface Notifier
    {
        void Notify<T>(T @event);
    }
}
