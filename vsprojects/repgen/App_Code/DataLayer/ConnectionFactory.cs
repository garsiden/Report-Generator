// $Id$
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;

namespace RSMTenon.RepGen.DataLayer
{

    /// <summary>
    /// Summary description for ConnectionFactory
    /// </summary>
    public class ConnectionFactory
    {
        static public SqlConnection CreateSqlConnection()
        {

            string appName = System.Web.HttpContext.Current.Request.ApplicationPath.Substring(1);
            string dbName;

            dbName = ConfigurationSettings.AppSettings["DbDefault"];

            return CreateSqlConnection(dbName);
        }

        static public SqlConnection CreateSqlConnection(string dbName)
        {

            NameValueCollection appSettings = (NameValueCollection)ConfigurationManager.GetSection("appSettings");
            string server = appSettings["DbServer"];
            string user = appSettings["DbUser"];
            string pwd = appSettings["DbPwd"];

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "SERVER=" + server +
                                    ";DATABASE=" + dbName +
                                    ";UID=" + user +
                                    ";PWD=" + pwd;
            return conn;
        }
    }
}