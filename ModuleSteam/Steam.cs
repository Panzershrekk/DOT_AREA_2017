using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;
using PortableSteam;
using PortableSteam.Fluent.General.ISteamUser;
using PortableSteam.Infrastructure.Converters;
using PortableSteam.Interfaces.General.ISteamUser;

namespace Module
{
    public class ModuleSteam : AModule
    {
        public override string GetRequest()
        {
            //535AB6031548709DFEEB34CABB80601E
            SteamWebAPI.SetGlobalKey("535AB6031548709DFEEB34CABB80601E");
            var response = SteamWebAPI.General().IPlayerService()
                .GetOwnedGames(SteamIdentity.FromAccountID(76561197998335096)).GetResponseString();
            /*.Game().Generic()*/
                /*.IEconItems(GameType.Portal2)
                .GetPlayerItems(SteamIdentity.FromSteamID(76561197998335096))
                .GetResponse();*/

            return (response);
            //return JsonConvert.SerializeObject(response, new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }
    }
}
