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
            if (!collection.ContainsKey("message"))
                JsonConvert.SerializeObject("KO", new JsonApiSerializerSettings());
            var message = collection["message"];
            Area.Modules[typeof(ModuleTwitter)].TwitterPostRequest(message);
            Area.Linker.ExecuteReactions("TwitterPostRequest",
                Area.User, message);
            return JsonConvert.SerializeObject("OK", new JsonApiSerializerSettings());
        }
    }
}