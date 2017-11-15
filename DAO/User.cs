namespace DAO
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(long id = 0, string username = null, string password = null)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public string prepareInsertQuery()
        {
            var query = "INSERT INTO users (username, password)" +
                        $"VALUES ('{Username}', '{Password}');";
            return query;
        }
    }
}