using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameStore
{
    internal class DataBase
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "GameStoreARM";
            string username = "root";
            string password = "";

            return RegForm.GetDBConnection(host, port, database, username, password);

        }
    }

    internal class userInfo
    {
        public static string fioUser;
        public static string roleUser;
    }
}
