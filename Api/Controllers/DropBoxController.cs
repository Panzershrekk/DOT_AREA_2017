using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Module;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class DropBoxController : Controller
    {
        private DropBox Module { get; set; }

        public DropBoxController()
        {
            Module = new Module.DropBox();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public string Index()
        {
            return Module.GetFolderList();
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
            
            var signature = sig.FirstOrDefault();
            string body = null;
            using (var reader = new StreamReader(Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }
            var appSecret = "4fa52tui944xzpx";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(appSecret)))
            {
                if (!DropBox.VerifySha256Hash(hmac, body, signature))
                    return Content("400 Bad Request");
            }
            var decoded = JsonConvert.DeserializeObject<JObject>(body, new JsonSerializerSettings());
            var account = decoded["list_folder"]["accounts"].Last.ToString();
            var name = Module.GetNameAccount(account);
            Console.WriteLine("Modification dropbox on the " + name + " account");
            return Content("200 OK");
        }
    }
}
