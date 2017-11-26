using JsonApiSerializer;
using LinkerModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HttpRequest = HttpResponse.HttpRequest;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class LinkerController : Controller
    {
        public Linker Linker { get; set; }

        public LinkerController()
        {
            
        }

        [HttpPost]
        public string MakeLink(IFormCollection collection)
        {
            var response = new HttpResponse.HttpRequest();
            if (!collection.ContainsKey("act") ||
                !collection.ContainsKey("react"))
            {
                response.Message =
                    "Your request must provide act / react fields";
                return response.ToJson();
            }
            var act = collection["act"];
            var react = collection["react"];
            if (!Linker.AddLink(act, react))
            {
                response.Message = "Invalid link has been asked";
                return response.ToJson();
            }
            response.Status = "OK";
            return response.ToJson();
        }

        [HttpDelete]
        public string DeleteLink(IFormCollection collection)
        {
            var response = new HttpResponse.HttpRequest();
            if (!collection.ContainsKey("act") ||
                !collection.ContainsKey("react"))
            {
                response.Message =
                    "Your request must provide act / react fields";
                return response.ToJson();
            }
            var act = collection["act"];
            var react = collection["react"];
            if (!Linker.DeleteLink(act, react))
            {
                response.Message = "Invalid link has been asked to be delete";
                return response.ToJson();
            }
            response.Status = "OK";
            return response.ToJson();
        }
    }
}