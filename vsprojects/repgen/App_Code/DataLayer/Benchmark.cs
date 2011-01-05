using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace RSMTenon.Data
{
    [System.ComponentModel.DataObject]
    public partial class Benchmark
    {
        public static void UploadToDatabase(DataUpload.BenchmarkDataTable dt)
        {
            string tableName = "tblBenchmarkDataTest";
            string sql = String.Format("DELETE FROM {0}", tableName);

            using (SqlConnection cn = ConnectionFactory.CreateSqlConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                int deleted = cmd.ExecuteNonQuery();
                Console.WriteLine("{0} record(s) deleted", deleted);

                using (SqlBulkCopy bc = new SqlBulkCopy(cn))
                {
                    bc.DestinationTableName = tableName;
                    bc.WriteToServer(dt);
                }
            }
        }

    }

}