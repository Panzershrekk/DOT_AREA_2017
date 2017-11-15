using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class Database
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Basename { get; set; }
        public MySqlConnection Connection { get; set; }
        
        ~Database()
        {
            Connection.Close();
        }
        
        public Database(MySqlConnection connection)
        {
            Connection = connection;
            try
            {
                Connection.Open();
            }
            catch (Exception e)
            {
                throw new DaoException($"{e.Message}");
            }
        }

        public IEnumerable<dynamic> Query<T>(string sql, object param = null)
        {
            return (IEnumerable<dynamic>) Connection.Query<T>(sql);
        }
        
        public int Execute(string sql,
            object param = null, SqlTransaction transaction = null)
        {
            return Connection.Execute(sql, param, transaction);
        }
    }
}