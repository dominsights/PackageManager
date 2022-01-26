using DgSystems.PackageManager.Controllers.InstallPackage;
using DgSystems.PackageManager.Presenters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DgSystems.PackageManager.WebAPI.Install
{
    [Route("api/install")]
    [ApiController]
    public class InstallApiController : ControllerBase, Observer
    {
        private readonly PackageManagerFactory packageManagerFactory;
        private readonly Notifier notifier;

        public InstallApiController(PackageManagerFactory packageManagerFactory, Notifier notifier)
        {
            this.packageManagerFactory = packageManagerFactory;
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
            var presenter = new Presenters.InstallPackage.Presenter();
            presenter.Attach(this);
            var interactor = new UseCases.InstallPackage.InstallPackageInteractor(presenter, packageManagerFactory.Create(), notifier);
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

        [NonAction]
        public void Update(Subject subject)
        {
            if (subject is Presenters.InstallPackage.Presenter presenter)
            {
                string json = JsonConvert.SerializeObject(presenter.InstallPackageOutput);
                Response.WriteAsync(json); //TODO: reply with signalR
            }
        }
    }
}
