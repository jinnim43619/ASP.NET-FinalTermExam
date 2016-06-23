using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_FinalTermExam.Models
{
    public class GetInfo
    {
        /// <summary>
        /// 取得DB連線
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }
        public List<SelectListItem> GetTitle()
        {
            DataTable dt = new DataTable();
            string sql = @"Select CodeId ,CodeVal FROM dbo.CodeTable WHERE CodeType='TITLE' ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            result.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeVal"].ToString(),
                    Value = row["CodeId"].ToString()
                });
            }
            return result;
        }
    }
}