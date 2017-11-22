using System;
using Microsoft.AspNetCore.Mvc;
using Module;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class FacebookController : Controller
    {

        public FacebookController()
        {
        }
        
        [HttpGet]
        public string Index()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public string Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }
    }
}