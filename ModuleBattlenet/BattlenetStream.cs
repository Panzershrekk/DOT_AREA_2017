using System;
using System.Threading;
using System.Threading.Tasks;
using ArgentPonyWarcraftClient;

namespace Module
{
    class BattlenetStram
    {
        public BattlenetStram()
        {
            Task task = new Task(new Action(CompareStatChar));
            task.Start();
        }

        public async void CompareStatChar()
        {
            var warcraftClient = new WarcraftClient("g4bzbwkn3ejumdtbqunxm2eu8xs7mu4m", Region.Europe, "fr_Fr");
            Character character = await warcraftClient.GetCharacterAsync("Culte de la Rive Noire", "Sundstrom", CharacterFields.All);
            var level = character.Level;
            var achievement = character.AchievementPoints;
            while (true)
            {
                Thread.Sleep(1000);
                Character newcharacter = await warcraftClient.GetCharacterAsync("Culte de la Rive Noire", "Sundstrom", CharacterFields.All);
                var newLevel = newcharacter.Level;
                var newAchievement = newcharacter.AchievementPoints;
                if (newLevel > level)
                {
                    Console.WriteLine("Je suis passé niveau " + newLevel + " sur " + newcharacter.Name);
                    level = newLevel;
                }
                if (newAchievement > achievement)
                {
                    Console.WriteLine("Je suis passé à " + newAchievement + " point de haut fait " + newcharacter.Name);
                    achievement = newAchievement;
                }

            }
        }
    }
}
