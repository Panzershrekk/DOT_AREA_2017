using System.Threading.Tasks;
using JsonApiSerializer;
using Newtonsoft.Json;
using ArgentPonyWarcraftClient;
using Character = ArgentPonyWarcraftClient.Character;
using CharacterFields = ArgentPonyWarcraftClient.CharacterFields;
using Region = ArgentPonyWarcraftClient.Region;

namespace Module
{
    public class ModuleBattlenet : AModule
    {
        public int BattlenetGetPoints()
        {
            var response = BattlenetGetInfo().Result.AchievementPoints;
            return response;
        }

        private static async Task<Character> BattlenetGetInfo()
        {
            var warcraftClient = new WarcraftClient("g4bzbwkn3ejumdtbqunxm2eu8xs7mu4m", Region.Europe, "fr_Fr");
            var character = await warcraftClient.GetCharacterAsync("Culte de la Rive Noire", "Sundstrom", CharacterFields.All);
            return (character);
        }
    }
}
