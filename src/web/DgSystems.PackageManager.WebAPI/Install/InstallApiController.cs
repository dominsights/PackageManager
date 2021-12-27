using DgSystems.PackageManager.Controllers.InstallPackage;
using DgSystems.PackageManager.Presenters.InstallPackage;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DgSystems.PackageManager.WebAPI.Install
{
    [Route("api/install")]
    [ApiController]
    public class InstallApiController : ControllerBase
    {
        private readonly InstallController installController;
        private readonly Entities.PackageManager packageManager;
        private readonly Notifier notifier;

        public InstallApiController(InstallController installController, Entities.PackageManager packageManager, Notifier notifier)
        {
            this.installController = installController;
            this.packageManager = packageManager;
            this.notifier = notifier;
        }

        // GET: api/<InstallController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InstallController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InstallController>
        [HttpPost]
        public void Post([FromBody] InstallPackageInput input)
        {
            var presenter = new InstallPackagePresenter(Response);
            var interactor = new UseCases.InstallPackage.Interactor(presenter, packageManager, notifier);
            var installController = new InstallController(interactor);
            installController.Install(input.Name, input.Path, input.FileName);
        }

        // PUT api/<InstallController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InstallController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
