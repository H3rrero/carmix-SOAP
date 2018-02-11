using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarMix.data
{
    public static class DBConect
    {
        public static MySqlConnection Conect()
        {
            string connStr = "server=localhost;user=root;database=carmix;port=3306;";
            MySqlConnection conn = new MySqlConnection(connStr);
            return conn;
        }
    }

    }
