using System;
using System.Reflection.Metadata;
using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc;
using Module;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class GmailController : Controller
    {
        private ModuleGmail Module { get; set; }

        public GmailController()
        {
            Module = new ModuleGmail();
        }

        [HttpGet]
        public string Index()
        {
            throw new NotImplementedException();
        }

        [HttpGet("getLabel")]
        public string GetLabel()
        {
            return Module.GetLabel();
        }

        [HttpGet("getMessage")]
        public string GetMessage(string subject)
        {
            return Module.GetMessage(subject);
        }

        [HttpPost("send")]
        public string SendMessage(string dest, string subject, string body)
        {
            return JsonConvert.SerializeObject(Module.SendMessage(dest, subject, body) ? "OK" : "Error", new JsonApiSerializerSettings());
        }
    }
}