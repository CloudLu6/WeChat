using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SuggestionBox.DB_APP
{
    public class BaseClass
    {
        public BaseClass()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public static SqlConnection DBCon()
        {
            return new SqlConnection("Data Source=10.116.17.148;Initial Catalog=SHAWechart;uid=wechart;pwd=wechart;");
        }

        public static void BindDG(GridView dg, string id, string strSql, string Tname)
        {
            SqlConnection conn = DBCon();
            SqlDataAdapter sda = new SqlDataAdapter(strSql, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, Tname);
            dg.DataSource = ds.Tables[Tname];
            dg.DataKeyNames = new string[] { id };
            dg.DataBind();
        }
        public static void OperateData(string strsql)
        {
            SqlConnection conn = DBCon();
            conn.Open();
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.Transaction = conn.BeginTransaction();//定义事务
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();//执行完成，事务提交
            }

            catch (Exception ex)
            {
                cmd.Transaction.Rollback();//中途出错，事务回滚
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}