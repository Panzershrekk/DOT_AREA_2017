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
                var response =
                    new HttpResponse.HttpRequest("KO",
                        e.Message);
                return response.ToJson();
            }
        }

        [HttpPost("message")]
        public string SendMessage(IFormCollection collection)
        {
            var response = new HttpResponse.HttpRequest();
            if (!collection.ContainsKey("dest") ||
                !collection.ContainsKey("subject") ||
                !collection.ContainsKey("body"))
            {
                response.Message = "Your request must be param by a dest/subject/body";
                return response.ToJson();
            }
            var dest = collection["dest"];
            var subject = collection["subject"];
            var body = collection["body"];

            if (!Area.Modules[typeof(ModuleGmail)]
                .GmailSendMessage(dest, subject, body))
            {
                response.Message =
                    "An error has been reached when try to send the message, " +
                    "please try later";
                return response.ToJson();
            }
            response.Status = "OK";
            return response.ToJson();
        }
    }
}