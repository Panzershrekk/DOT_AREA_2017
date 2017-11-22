using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc;
using Module;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class GmailController : Controller
    {
        [HttpGet("getLabel")]
        public string GetLabel()
        {
            return Area.Modules[typeof(ModuleGmail)].GmailGetLabel();
        }

        [HttpGet("getMessage")]
        public string GetMessage(string subject)
        {
            return Area.Modules[typeof(ModuleGmail)].GmailGetMessage(subject);
        }

        [HttpPost("send")]
        public string SendMessage(string dest, string subject, string body)
        {
            return JsonConvert.SerializeObject(Area.Modules[typeof(ModuleGmail)].GmailSendMessage(dest, subject, body) ? "OK" : "Error", new JsonApiSerializerSettings());
        }
    }
}