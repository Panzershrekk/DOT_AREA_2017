using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class FacebookController : Controller
    {
        private Module.ModuleFacebook module { get; set; }

        public FacebookController()
        {
            module = new Module.ModuleFacebook();
        }
        
        [HttpGet]
        public string Index()
        {
            return module.GetRequest();
        }

        [HttpPost]
        public string Post([FromBody] string value)
        {
            return module.PostRequest();
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