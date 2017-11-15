using System;
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


        [HttpPost("Post")]
        public string Post(string Name)
        {
            return module.PostRequest(Name);
        }
    }
}