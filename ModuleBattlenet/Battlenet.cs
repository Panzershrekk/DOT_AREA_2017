using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BattleDotNet;
using BattleDotNet.Objects.WoW;
using BattleNetAPI;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;
using ArgentPonyWarcraftClient;
using Character = ArgentPonyWarcraftClient.Character;
using CharacterFields = ArgentPonyWarcraftClient.CharacterFields;
using Region = ArgentPonyWarcraftClient.Region;

namespace Module
{
    public class ModuleBattlenet : AModule
    {
        public override string GetRequest()
        {
            var response = GetInfo().Result.AchievementPoints;
            if (response.Equals(""))
                return ("Error");
            return JsonConvert.SerializeObject(response, new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }

        public async Task<Character> GetInfo()
        {
            var warcraftClient = new WarcraftClient("g4bzbwkn3ejumdtbqunxm2eu8xs7mu4m", Region.Europe, "fr_Fr");
            Character character = await warcraftClient.GetCharacterAsync("Culte de la Rive Noire", "Sundstrom", CharacterFields.All);
            return (character);
        }
    }
}
