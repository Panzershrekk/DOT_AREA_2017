using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArgentPonyWarcraftClient;
using DAO;
using LinkerModule;
using Module;
using Tweetinvi;
using Thread = System.Threading.Thread;
using User = DAO.User;

namespace Api
{
    public class Area
    {
        public static Linker Linker { get; set; }
        public static User User { get; set; }
        public static Database Database { get; set; }
        public static Dictionary<Type, dynamic> Modules { get; set; }

        public Area()
        {
            Init();
        }

        private void StreamTwitter()
        {
            Auth.SetUserCredentials("hAVBTJykgQyF6bkxsSNmTs7mj",
                "dnr2QSlGlq5dyiecgZqLDBdtqYpfXN7a5MCwH9AkgYAozgrBJ6",
                "922446818033664001-wwrq7uhrWDJGdrWONt9W1n9208KrSER",
                "4Yx5KgWPmgpzWQ2AEzN58bykrmPiMrZr9TSoYuKSH28hP");

            var stream = Stream.CreateUserStream();
            stream.TweetCreatedByMe += (sender, args) =>
            {
                try
                {
                    Linker.ExecuteReactions("TwitterPostRequest", User,
                        args.Tweet.Text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            };
            stream.StartStreamAsync();
        }
        
        public async Task StreamBattlenet()
        {
            var warcraftClient = new WarcraftClient("g4bzbwkn3ejumdtbqunxm2eu8xs7mu4m", Region.Europe, "fr_Fr");
            var character = await warcraftClient.GetCharacterAsync("Culte de la Rive Noire", "Sundstrom", CharacterFields.All);
            var level = character.Level;
            var achievement = character.AchievementPoints;
            while (true)
            {
                Thread.Sleep(1000);
                var newcharacter = await warcraftClient.GetCharacterAsync("Culte de la Rive Noire", "Sundstrom", CharacterFields.All);
                var newLevel = newcharacter.Level;
                var newAchievement = newcharacter.AchievementPoints;
                if (newLevel > level)
                {
                    level = newLevel;
                }
                if (newAchievement <= achievement) continue;
                Console.WriteLine("Je suis passé à " + newAchievement + " point de haut fait " + newcharacter.Name);
                achievement = newAchievement;
            }   
        }
        
        private void InitDatabase()
        {
            var daoFactory = new DaoFactory("database.properties");
            Database = daoFactory.GetInstance();

            const string initTableRequest = "CREATE TABLE IF NOT EXISTS `user` (" +
                                            "`id` int(11) NOT NULL AUTO_INCREMENT," +
                                            "`username` varchar(255) DEFAULT NULL," +
                                            "`password` varchar(255) DEFAULT NULL," +
                                            "`firstname` varchar(255) DEFAULT NULL," +
                                            "`lastname` varchar(255) DEFAULT NULL," +
                                            "`email` varchar(255) DEFAULT NULL," +
                                            "PRIMARY KEY (`id`)" +
                                            ") ENGINE=MyISAM AUTO_INCREMENT=0 DEFAULT CHARSET=latin1;";
            Database.Execute(initTableRequest);
        }

        private void InitModules()
        {
            Modules = new Dictionary<Type, dynamic>
            {
                {typeof(ModuleTwitter), new ModuleTwitter()},
                {typeof(ModuleFacebook), new ModuleFacebook()},
                {typeof(ModuleGmail), new ModuleGmail()},
                {typeof(ModuleSteam), new ModuleSteam()},
                {typeof(ModuleBattlenet), new ModuleBattlenet()},
                {typeof(ModuleDropbox), new ModuleDropbox()}
            };
        }

        private void InitLinker()
        {
            Linker = new Linker(Modules);
        }

        private void InitUser()
        {
            //TODO : Guillaume - get user id = 1;
            User = new User(1,
                "guillaume.cauchois",
                "password",
                "Guillaume",
                "CAUCHOIS",
                "guillobits@gmail.com");
        }
        
        public void Init()
        {
            InitDatabase();
            InitModules();
            InitLinker();
            InitUser();
            StreamTwitter();
            StreamBattlenet();
        }
    }
}