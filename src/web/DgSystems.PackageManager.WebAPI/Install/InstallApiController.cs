using DgSystems.PackageManager.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DgSystems.PackageManager.WebAPI.Install
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallApiController : ControllerBase
    {
        private readonly InstallController installController;

        public InstallApiController(InstallController installController)
        {
            this.installController = installController;
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
        public void Post([FromBody] string value)
        {
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
