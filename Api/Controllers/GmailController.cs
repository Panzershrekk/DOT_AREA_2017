using System;
using JsonApiSerializer;
using Microsoft.AspNetCore.Http;
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
            return label;
        }

        [HttpGet("message/{nb}")]
        public string GetMessage(int nb)
        {
            try
            {
                var messages = Area.Modules[typeof(ModuleGmail)]
                    .GmailGetMessage(nb);
                var jsonMessages = JsonConvert.SerializeObject(messages,
                    new JsonApiSerializerSettings());
                Area.Linker.ExecuteReactions("GmailGetMessage", Area.User, jsonMessages);
                return jsonMessages;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            return "ERROR";
        }

        [HttpPost("message")]
        public string SendMessage(IFormCollection collection)
        {
            if (!collection.ContainsKey("dest") ||
                !collection.ContainsKey("subject") ||
                !collection.ContainsKey("body"))
            {
                return "ERROR";
            }
            var dest = collection["dest"];
            var subject = collection["subject"];
            var body = collection["body"];
            return JsonConvert.SerializeObject(Area.Modules[typeof(ModuleGmail)].GmailSendMessage(dest, subject, body) ? "OK" : "Error", new JsonApiSerializerSettings());
        }
    }
}