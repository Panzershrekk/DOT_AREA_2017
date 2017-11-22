using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BattlenetController : Controller
    {
        private Module.ModuleBattlenet module { get; set; }

        public BattlenetController()
        {
            module = new Module.ModuleBattlenet();
        }

        [HttpGet]
        public string Index()
        {
            return module.GetRequest();
        }


        [HttpPost("Post")]
        public string Post()
        {
            return module.PostRequest();
        }
    }
}