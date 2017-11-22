using Microsoft.AspNetCore.Mvc;
using Module;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class SteamController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return Area.Modules[typeof(ModuleSteam)].SteamGetFriendList();
        }
    }
}