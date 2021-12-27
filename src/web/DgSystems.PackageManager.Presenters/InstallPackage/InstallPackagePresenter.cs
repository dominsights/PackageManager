using DgSystems.PackageManager.UseCases.InstallPackage;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DgSystems.PackageManager.Presenters.InstallPackage
{
    public class InstallPackagePresenter : OutputBoundary
    {
        private readonly HttpResponse httpResponse;

        public InstallPackagePresenter(HttpResponse httpResponse)
        {
            this.httpResponse = httpResponse;
        }

        public void PresentAsync(Response installPackageResponse)
        {
            var output = new InstallPackageOutput(installPackageResponse.PackageName, installPackageResponse.Message);
            string json = JsonConvert.SerializeObject(output);
            httpResponse.WriteAsync(json);
        }
    }
}
