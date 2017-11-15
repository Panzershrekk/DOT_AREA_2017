using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class SteamController : Controller
    {
        private Module.ModuleSteam module { get; set; }

        public SteamController()
        {
            module = new Module.ModuleSteam();
        }

        [HttpGet]
        public string Index()
        {
            return module.GetRequest();
        }

        /*
        [HttpGet("toto/{id}")]
        public string Get(int id)
        {
            return $"toto-id = {id}";
        }
        */
    }
}