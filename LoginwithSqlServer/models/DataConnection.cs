using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginwithSqlServer
{
    class DataConnection
    {
        public static SqlConnection DataCon { get; set; }
        public static void ConnectionDB(string ip,string dbname,string username,string password)
        {
            string connectionString = "Server = "+ip+"; Database = "+dbname+"; User Id = "+username+"; Password = "+password+";";
            DataCon = new SqlConnection(connectionString);
            DataCon.Open();
        }
        public static void ConnectionDB(string ip, string dbname)
        {
            string ConnectionString = "Server = " + ip + "; Database = " + dbname + "; Trusted_Connection = True;";
            DataCon = new SqlConnection(ConnectionString);
            DataCon.Open();
        }

    }
}
