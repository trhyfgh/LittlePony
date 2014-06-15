using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LTDBAccess
{
    public class DBHelper
    {
        public DataTable ExeSQL(string strSql)
        {
            string connStr = "Data Source=pq7f8md6ns.database.chinacloudapi.cn;Initial Catalog=LittlePony;Persist Security Info=True;User ID=trhyfgh;Password=Fgh6890259";
            SqlConnection sqlConn = new SqlConnection(connStr);
            DataTable dt = new DataTable();
            try
            {
                sqlConn.Open();

                SqlDataAdapter sda = new SqlDataAdapter(strSql, sqlConn);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                
                dt = ds.Tables[0];
            }
            catch {
                dt = null;
            }
               

            return dt;
        }
        public bool ExcuteChangeSql(string strSql)
        {
            bool result = false;
            bool sqlOpened = false;

            string connStr = "Data Source=pq7f8md6ns.database.chinacloudapi.cn;Initial Catalog=LittlePony;Persist Security Info=True;User ID=trhyfgh;Password=Fgh6890259";
            SqlConnection sqlConn = new SqlConnection(connStr);
            try
            {
                sqlConn.Open();
                sqlOpened = true;
                SqlCommand cmd = new SqlCommand(strSql, sqlConn);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    result = true;
                sqlConn.Close();

            }
            catch
            {
                result = false;
            }
            finally
            {
                if (sqlOpened)
                    sqlConn.Close();
            }
            
            return result;
        }
    }
}
