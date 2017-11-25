using Newtonsoft.Json;

namespace DAO
{
    public class User
    {
        [JsonProperty()]
        public int Id { get; set; }
        [JsonProperty()]
        public string Username { get; set; }
        [JsonProperty()]
        public string Password { get; set; }
        [JsonProperty()]
        public string Lastname { get; set; }
        [JsonProperty()]
        public string Firstname { get; set; }
        [JsonProperty()]
        public string Email { get; set; }
        
        public User(int id = 0, string username = null, string password = null,
            string firstname = null, string lastname = null, string email = null)
        {
            Id = id;
            Username = username;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
        }

        public int InsertInDatabase(Database db)
        {
            return db.Execute(
                "insert user(username, password, email, firstname, lastname) " +
                $"values ('{Username}', '{Password}', '{Email}', '{Firstname}', '{Lastname}')"
            );
        }
    }
}