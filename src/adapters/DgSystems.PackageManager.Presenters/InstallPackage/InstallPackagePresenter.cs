using DgSystems.PackageManager.UseCases.InstallPackage;

namespace DgSystems.PackageManager.Presenters.InstallPackage
{
    public class InstallPackagePresenter : InstallPackageOutputBoundary, Subject
    {
        public InstallPackageOutput InstallPackageOutput { get; set; }
        private readonly List<Observer> observers = new List<Observer>();

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

        public void PresentAsync(InstallPackageResponse installPackageResponse)
        {
            InstallPackageOutput = new InstallPackageOutput(installPackageResponse.PackageName, installPackageResponse.Message);
            Notify();
        }
    }
}
