using JsonApiSerializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class FacebookController : Controller
    {
        [HttpGet("me")]
        public string Index()
        {
            var ret = Area.Modules[typeof(ModuleFacebook)].FacebookGetMe();
            var jsonObj =
                JsonConvert.SerializeObject(ret,
                    new JsonApiSerializerSettings());
            Area.Linker.ExecuteReactions("FacebookGetMe", Area.User, jsonObj);
            return JsonConvert.SerializeObject(ret,
                new JsonApiSerializerSettings());
        }

        [HttpPost("post")]
        public string PostStatus(IFormCollection collection)
        {
            var httpResponse = new HttpResponse.HttpRequest();
            if (!collection.ContainsKey("message"))
            {
                httpResponse.Message =
                    "The post request must specified the message field";
                return httpResponse.ToJson();
            }
            var message = collection["message"];
            if (!Area.Modules[typeof(ModuleFacebook)].FacebookPostStatus(message))
            {
                httpResponse.Message =
                    "An error occured when try to post on Facebook";
                return httpResponse.ToJson();
            }
            httpResponse.Status = "OK";
            Area.Linker.ExecuteReactions("FacebookPostStatus", Area.User,
                message);
            return httpResponse.ToJson();
        }

        public string VerifWebhook()
        {
            var data = Request.Query;
            if (data["hub.mode"] == "subscribe" && data["hub.verify_token"] == "123456789")
            {
                var retVal = data["hub.challenge"];
                return retVal;
            }
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }

        [HttpPost("Webhook")]
        public string Webhook(string message)
        {
            return Area.Modules[typeof(ModuleFacebook)].Webhook(message);
        }
    }
}