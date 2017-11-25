using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ModuleBattlenet = Module.ModuleBattlenet;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BattlenetController : Controller
    {
        [HttpGet("points")]
        public string GetPoints()
        {
            var points = Area.Modules[typeof(ModuleBattlenet)].BattlenetGetPoints();
            var strPoints = points.ToString();
            Area.Linker.ExecuteReactions("BattlenetGetPoints", Area.User, strPoints);
            return JsonConvert.SerializeObject(points, new JsonApiSerializer.JsonApiSerializerSettings());
        }
    }
}