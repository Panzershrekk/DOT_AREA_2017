using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc;
using Module;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class GmailController : Controller
    {
        [HttpGet("label")]
        public string GetLabel()
        {
            var label = Area.Modules[typeof(ModuleGmail)].GmailGetLabel();
            Area.Linker.ExecuteReactions("GmailGetLabel", Area.User, label);
            return JsonConvert.SerializeObject(label,
                new JsonApiSerializerSettings());
        }

        [HttpGet("message")]
        public string GetMessage(string subject)
        {
            var messages = Area.Modules[typeof(ModuleGmail)]
                .GmailGetMessage(subject);
            Area.Linker.ExecuteReactions("GmailGetMessage", Area.User, messages);
            return JsonConvert.SerializeObject(messages,
                new JsonApiSerializerSettings());
        }

        [HttpPost("message")]
        public string SendMessage(string dest, string subject, string body)
        {
            return JsonConvert.SerializeObject(Area.Modules[typeof(ModuleGmail)].GmailSendMessage(dest, subject, body) ? "OK" : "Error", new JsonApiSerializerSettings());
        }
    }
}