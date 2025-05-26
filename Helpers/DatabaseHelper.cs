using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ETR_IPT3D_TEAM3.Helpers
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
