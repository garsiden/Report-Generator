using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;

namespace RSMTenon.Data
{
    public class ConnectionFactory
    {
        static public SqlConnection CreateSqlConnection()
        {
            string defaultConnection = ConfigurationSettings.AppSettings["DefaultConnection"];
            return CreateSqlConnection(defaultConnection);
        }

        static public SqlConnection CreateSqlConnection(string connectionStringName)
        {
            ConnectionStringSettingsCollection cs = ConfigurationManager.ConnectionStrings;
            string connString = cs[connectionStringName].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }
    }
}