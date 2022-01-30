using DgSystems.PackageManager.Controllers.UninstallPackage;
using DgSystems.PackageManager.Presenters;
using DgSystems.Scoop;
using Microsoft.AspNetCore.Mvc;

namespace DgSystems.PackageManager.WebAPI.Uninstall;

[Route("api/uninstall")]
[ApiController]
public class UninstallApiController : Observer
{
    private readonly ScoopFactory scoopFactory;

    public UninstallApiController(ScoopFactory scoopFactory)
    {
        this.scoopFactory = scoopFactory;
    }

    // POST api/<InstallController>
    [HttpPost]
    public void Post([FromBody] string packageName)
    {
        var presenter = new Presenters.UninstallPackage.UninstallPackagePresenter();
        presenter.Attach(this);
        var interactor = new UseCases.UninstallPackage.UninstallPackageInteractor(presenter, scoopFactory.Create());
        var uninstallController = new UninstallController(interactor);
        uninstallController.UninstallAsync(packageName);
    }

    public void Update(Subject subject)
    {
        throw new NotImplementedException();
    }
}