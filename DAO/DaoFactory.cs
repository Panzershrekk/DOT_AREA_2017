using System;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class DaoFactory
    {
        private static string ipProperties = "ip";
        private static string portProperties = "port";
        private static string databaseProperties = "database";
        private static string usernameProperties = "username";
        private static string passwordProperties = "password";

        public string PathProperties { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public DaoFactory(string pathProperties)
        {
            PathProperties = pathProperties;
        }

        public Database GetInstance()
        {
            var properties = new Properties.Properties(PathProperties);

            Ip = properties.GetValue(ipProperties);
            Port = properties.GetValue(portProperties);
            Database = properties.GetValue(databaseProperties);
            Username = properties.GetValue(usernameProperties);
            Password = properties.GetValue(passwordProperties);

            var connectionString =
                $"Server={Ip}; database={Database}; " +
                $"UID={Username}; password={Password}; " +
                "Allow User Variables=True";
            var connection = new MySqlConnection(connectionString);
            var database = new Database(connection)
            {
                Ip = Ip,
                Port = Port,
                Basename = Database,
                Username = Username,
                Connection = connection
            };
            
            return database;
        }
    }
}