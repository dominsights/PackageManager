using DgSystems.PackageManager.UseCases.UninstallPackage;

namespace DgSystems.PackageManager.Presenters.UninstallPackage
{
    public class UninstallPackagePresenter : UninstallPackageOutputBoundary, Subject
    {
        public UninstallPackageOutput? Output { get; set; }
        public void Attach(Observer observer)
        {
            throw new NotImplementedException();
        }

        public void Detach(Observer observer)
        {
            throw new NotImplementedException();
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }

        public void PresentAsync(UninstallPackageResponse uninstallPackageResponse)
        {
            throw new NotImplementedException();
        }
    }
}
