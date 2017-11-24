using JsonApiSerializer;
using LinkerModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class LinkerController : Controller
    {
        public Linker Linker { get; set; }

        public LinkerController()
        {
            Linker = Area.Linker;
        }

        [HttpPost]
        public string MakeLink(IFormCollection collection)
        {
            if (!collection.ContainsKey("act") || !collection.ContainsKey("react"))
                return JsonConvert.SerializeObject("KO",
                    new JsonApiSerializerSettings());
            var act = collection["act"];
            var react = collection["react"];
            return JsonConvert.SerializeObject((Linker.AddLink(act, react) ? "OK" : "KO"),
                new JsonApiSerializerSettings());
        }

        [HttpDelete]
        public string DeleteLink(IFormCollection collection)
        {
            if (!collection.ContainsKey("act") || !collection.ContainsKey("react"))
                return JsonConvert.SerializeObject("KO",
                    new JsonApiSerializerSettings());
            var act = collection["act"];
            var react = collection["react"];
            return JsonConvert.SerializeObject((Linker.DeleteLink(act, react) ? "OK" : "KO"),
                new JsonApiSerializerSettings());
        }
    }
}