using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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


        [HttpPost("Post")]
        public string PostStatus(string message)
        {
            return module.PostStatus(message);
        }

        [HttpGet("Webhook")]
        public string VerifWebhook(string message)
        {
            System.Console.WriteLine("Hello webhook");
            var data = Request.Query;
            if (data["hub.mode"] == "subscribe" && data["hub.verify_token"] == "123456789")
            {
                var retVal = data["hub.challenge"];
                return retVal;
             //   return JsonConvert.SerializeObject(retVal, new JsonApiSerializerSettings());
            }
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }

        [HttpPost("Webhook")]
        public string Webhook(string message)
        {
            return module.Webhook(message);
        }
    }
}