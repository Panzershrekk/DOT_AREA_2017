using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class TwitterController : Controller
    {
        private Module.ModuleTwitter module { get; set; }

        public TwitterController()
        {
            module = new Module.ModuleTwitter();
        }

        [HttpGet]
        public string Index()
        {
            return module.GetRequest();
        }

        [HttpGet("toto/{id}")]
        public string Get(int id)
        {
            return $"toto-id = {id}";
        }
    }
}