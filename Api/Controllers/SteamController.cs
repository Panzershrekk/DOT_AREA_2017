using Microsoft.AspNetCore.Mvc;
using Module;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class SteamController : Controller
    {
        private Module.ModuleSteam module { get; set; }

        public SteamController()
        {
            module = new Module.ModuleSteam();
        }

        [HttpGet]
        public string Index()
        {
            return ModuleSteam.GetFriendList();
        }
    }
}