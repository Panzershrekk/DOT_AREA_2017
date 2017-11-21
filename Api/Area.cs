using DAO;

namespace Api
{
    public class Area
    {
        private Database Database { get; set; }

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
                "PRIMARY KEY (`id`)" +
                ") ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;";
            
            Database.Execute(initTableRequest);
        }
        
        public void Init()
        {
            //InitDatabase();
        }
    }
}