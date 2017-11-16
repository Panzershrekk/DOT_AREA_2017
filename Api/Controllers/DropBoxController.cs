using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dropbox.Api;
using JsonApiSerializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class DropBoxController : Controller
    {
        private Module.DropBox Module { get; set; }

        public DropBoxController()
        {
            Module = new Module.DropBox();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public string Index()
        {
            return Module.GetRequest();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("webhook")]
        public ActionResult Dropbox(string challenge)
        {
            Console.WriteLine("webhook");
            return Content(challenge);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("webhook")]
        public async Task<ActionResult> Dropbox()
        {
            Microsoft.Extensions.Primitives.StringValues sig;
            var signatureHeader = Request.Headers.TryGetValue("X-Dropbox-Signature", out sig);
            if (!sig.Any())
                return Content("400 BadRequest");
            
            string signature = sig.FirstOrDefault();
            string body = null;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }
            string appSecret = "4fa52tui944xzpx";
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(appSecret)))
            {
                if (!Module.VerifySha256Hash(hmac, body, signature))
                    return Content("400 Bad Request");
            }
            var decoded = JsonConvert.DeserializeObject<JObject>(body, new JsonSerializerSettings());
            string account = decoded["list_folder"]["accounts"].Last.ToString();
            var name = Module.GetNameAccount(account);
            Console.WriteLine("Modification dropbox on the " + name + " account");
            return Content("200 OK");
        }
    }
}
