using System;
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
            if (!Area.Modules.ContainsKey(typeof(ModuleSteam)))
                throw new Exception("Cannot load the Steam module");
            var friendList = Area.Modules[typeof(ModuleSteam)].SteamGetFriendList();
            Area.Linker.ExecuteReactions("SteamGetFriendList", Area.User,
                friendList);
            return friendList;
        }
    }
}