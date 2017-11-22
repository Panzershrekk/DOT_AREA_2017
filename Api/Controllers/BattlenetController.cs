using System;
using Microsoft.AspNetCore.Mvc;
using ModuleBattlenet = Module.ModuleBattlenet;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BattlenetController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return Area.Modules[typeof(ModuleBattlenet)].GetRequest();
        }
    }
}