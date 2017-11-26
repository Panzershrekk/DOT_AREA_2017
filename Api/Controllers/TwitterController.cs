using JsonApiSerializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class TwitterController : Controller
    {
        [HttpGet("user")]
        public string GetUsername()
        {
            var username = Area.Modules[typeof(ModuleTwitter)].TwitterGetUsername();
            Area.Linker.ExecuteReactions("TwitterGetUsername", Area.User, username);
            return JsonConvert.SerializeObject(username, new JsonApiSerializerSettings());
        }

        [HttpPost("post")]
        public string PostTwit(IFormCollection collection)
        {
            var response = new HttpResponse.HttpRequest();
            if (!collection.ContainsKey("message"))
            {
                response.Message = "The request must provide a message field";
                return response.ToJson();
            }
            var message = collection["message"];
            Area.Modules[typeof(ModuleTwitter)].TwitterPostRequest(message);
            Area.Linker.ExecuteReactions("TwitterPostRequest",
                Area.User, message);
            response.Status = "OK";
            return response.ToJson();
        }
    }
}
