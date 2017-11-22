using Microsoft.AspNetCore.Mvc;
using Module;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class SteamController : Controller
    {
        [HttpGet("friends")]
        public string GetFriendList()
        {
            var friendList = Area.Modules[typeof(ModuleSteam)].SteamGetFriendList();
            Area.Linker.ExecuteReactions("SteamGetFriendList", Area.User,
                friendList);
            return friendList;
        }
    }
}