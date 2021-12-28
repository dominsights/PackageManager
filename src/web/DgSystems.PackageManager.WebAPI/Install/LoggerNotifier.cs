namespace DgSystems.PackageManager.WebAPI.Install
{
    // TODO: replace for real notifier when necessary
    public class LoggerNotifier : Notifier
    {
        public LoggerNotifier(ILogger<LoggerNotifier> logger)
        {
            Logger = logger;
        }

        public ILogger<LoggerNotifier> Logger { get; }

        public void Notify<T>(T @event)
        {
            Logger.LogInformation(@event.ToString());
        }
    }
}
