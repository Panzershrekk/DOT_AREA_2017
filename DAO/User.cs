namespace DAO
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public User(long id = 0, string username = null, string password = null,
            string firstname = null, string lastname = null)
        {
            Id = id;
            Username = username;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
        }

        public int InsertInDatabase(Database db)
        {
            return db.Execute(
                "insert users(username, password, firstname, lastname) " +
                $"values ('{Username}', '{Password}', '{Firstname}', '{Lastname}')"
            );
        }
    }
}