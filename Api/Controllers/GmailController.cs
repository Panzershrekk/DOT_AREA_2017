using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class GmailController : Controller
    {
        private Module.GmailModule module { get; set; }

        public GmailController()
        {
            module = new Module.GmailModule();
        }

        [HttpGet]
        public string Index()
        {
            return module.GetRequest();
        }

        [HttpGet("getLabel")]
        public string GetLabel()
        {
            return module.GetLabel();
        }

        [HttpGet("getMessage")]
        public string GetMessage(string subject)
        {
            return module.GetMessage(subject);
        }

        [HttpPost("send")]
        public string SendMessage(string dest, string subject, string body)
        {
            return module.SendMessage(dest, subject, body);
        }
    }
}