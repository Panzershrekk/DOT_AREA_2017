using System;
using System.Collections.Generic;
using DAO;
using LinkerModule;
using Module;
using ModuleBattlenet = Module.ModuleBattlenet;

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
                {typeof(ModuleDropbox), new ModuleDropbox()},
                {typeof(ModuleGmail), new ModuleGmail()},
                {typeof(ModuleSteam), new ModuleSteam()},
                {typeof(ModuleBattlenet), new ModuleBattlenet()},
            };
        }
        
        private void InitTwitterStreaming()
        {
            var module = (ModuleTwitter)Modules[typeof(ModuleTwitter)];
            module.TaskTweetReceived();            
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
            InitTwitterStreaming();
        }
    }
}