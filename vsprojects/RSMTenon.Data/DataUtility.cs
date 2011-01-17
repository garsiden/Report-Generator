using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace RSMTenon.Data
{
    public class DataUtilities
    {
        public static int UploadToDatabase(DataTable dt, string tableName)
        {
            string sql = String.Format("DELETE FROM {0}", tableName);
            int deleted = -1;

            using (SqlConnection cn = ConnectionFactory.CreateSqlConnection())
            {
                cn.Open();
                using (SqlTransaction tran = cn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand(sql, cn, tran);

                    using (SqlBulkCopy bc = new SqlBulkCopy(cn, SqlBulkCopyOptions.Default, tran))
                    {
                        bc.DestinationTableName = tableName;
                        try
                        {
                            deleted = cmd.ExecuteNonQuery();
                            bc.WriteToServer(dt);
                            tran.Commit();
                        } catch (Exception e)
                        {
                            tran.Rollback();
                            throw e;
                        }
                    }
                }
            }

            return deleted;
        }
    }
}