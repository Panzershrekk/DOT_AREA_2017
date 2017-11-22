using System;
using System.Collections.Generic;
using DAO;
using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc;
using Module;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class TwitterController : Controller
    {
        public TwitterController()
        {
            // if (!Area.Instance.Modules.ContainsKey(typeof(ModuleTwitter)))
            //   throw new Exception("Cannot load module");
        }

        [HttpGet]
        public string Index()
        {
            return "OK";
            //return Module.TwitterGetUsername();
        }


        [HttpPost("Post")]
        public string Post(string Name)
        {
            var ret = new List<string>();
            if (Area.Modules[typeof(ModuleTwitter)].TwitterPostRequest(Name))
                ret.Add(JsonConvert.SerializeObject("OK", new JsonApiSerializerSettings()));
            else
                ret.Add(JsonConvert.SerializeObject("KO", new JsonApiSerializerSettings()));
            ret.Add(
                JsonConvert.SerializeObject(Area.Linker.ExecuteReactions("TwitterPostRequest", Area.User, Name)));
            return JsonConvert.SerializeObject(ret, new JsonApiSerializerSettings());
        }
    }
}