using DgSystems.PackageManager.UseCases.UninstallPackage;

namespace DgSystems.PackageManager.Presenters.UninstallPackage
{
    public class UninstallPackagePresenter : UninstallPackageOutputBoundary, Subject
    {
        readonly List<Observer> observers = new List<Observer>();
        public UninstallPackageOutput? Output { get; set; }
        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            observers.ForEach(o => o.Update(this));
        }

        public void PresentAsync(UninstallPackageResponse uninstallPackageResponse)
        {
            throw new NotImplementedException();
        }
    }
}
