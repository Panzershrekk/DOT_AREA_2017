using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc;
using Module;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class DropboxController : Controller
    {
        [HttpGet("folder")]
        public string Folderlist()
        {
            var folderList = Area.Modules[typeof(ModuleDropbox)].DropboxGetFolderList();
            var jsonFolderList = JsonConvert.SerializeObject(folderList,
                new JsonApiSerializerSettings());
            Area.Linker.ExecuteReactions("DropboxGetFolderList", Area.User, jsonFolderList);
            return jsonFolderList;
        }

        [HttpGet("webhook")]
        public ActionResult Dropbox(string challenge)
        {
            return Content(challenge);
        }

        [HttpPost("webhook")]
        public async Task<string> Dropbox()
        {
            Console.WriteLine("POST Webhook");
            Request.Headers.TryGetValue("X-Dropbox-Signature", out var sig);
            if (!sig.Any())
                return "400 BadRequest";
            
            var signature = sig.FirstOrDefault();
            string body;
            using (var reader = new StreamReader(Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }
            const string appSecret = "4fa52tui944xzpx";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(appSecret)))
            {
                if (!ModuleDropbox.VerifySha256Hash(hmac, body, signature))
                    return "400 Bad Request";
            }
            var decoded = JsonConvert.DeserializeObject<JObject>(body, new JsonSerializerSettings());
            var account = decoded["list_folder"]["accounts"].Last.ToString();
            var names = Area.Modules[typeof(ModuleDropbox)].DropboxGetNameAccount(account);
            var json =
                JsonConvert.SerializeObject(names,
                    new JsonApiSerializerSettings());
            Area.Linker.ExecuteReactions("DropboxGetNameAccount", Area.User,
                json);
            Console.WriteLine(json);
            return json;
        }
    }
}
