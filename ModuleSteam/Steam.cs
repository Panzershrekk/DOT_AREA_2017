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
        public static string SteamGetFriendList()
        {
           SteamWebAPI.SetGlobalKey("535AB6031548709DFEEB34CABB80601E");
            var response = SteamWebAPI.General().ISteamUser().GetFriendList(SteamIdentity.FromSteamID(76561198062399869), RelationshipType.All).GetResponseString();
            return (response);
        }
    }
}
