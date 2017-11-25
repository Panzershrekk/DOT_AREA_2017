using System.IO;
using System.Linq;
using Dapper;
using DAO;
using Xunit;

namespace UnitTests
{
    public class Dao
    {
        private DaoFactory DaoFactory { get; set; }
        private Database Database { get; set; }
        
        public Dao()
        {
            const string filePath = "database.properties";
            
            File.Delete(filePath);
            using (var outputFile = new StreamWriter(filePath, true))
            {
                outputFile.WriteLine("ip=51.254.143.232");
                outputFile.WriteLine("port=3306");
                outputFile.WriteLine("database=areadotnet");
                outputFile.WriteLine("username=areadotnet");
                outputFile.WriteLine("password=DKRWUQC1npyZ9OWM");
            }
            DaoFactory = new DaoFactory(filePath);
            Database = DaoFactory.GetInstance();
        }
        
        [Fact]
        public void TestSimpleConnection()
        {
            Assert.True(true);
        }

        [Fact]
        public void InsertSimpleUserInDataBase()
        {
            var user = new User()
            {
                Username = "username",
                Password = "password",
                Firstname = "Guillaume",
                Lastname = "CAUCHOIS",
                Email = "guillaume.cauchois@epitech.eu"
            };

            Assert.Equal(user.InsertInDatabase(Database), 1);
        }

        [Fact]
        public void GenerateUserModelFromDb()
        {
            var user = Database.Query<User>("SELECT * FROM user", new { id = 3}).First();
            Assert.True(user != null);
        }
    }
}