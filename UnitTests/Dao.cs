using System.IO;
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
                outputFile.WriteLine("ip=localhost");
                outputFile.WriteLine("port=3306");
                outputFile.WriteLine("database=areadotnet");
                outputFile.WriteLine("username=areadotnet");
                outputFile.WriteLine("password=areadotnet");
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
            var user = new User
            {
                Username = "username",
                Password = "password",
                Firstname = "Guillaume",
                Lastname = "CAUCHOIS"
            };

            Assert.Equal(user.InsertInDatabase(Database), 1);
        }
    }
}