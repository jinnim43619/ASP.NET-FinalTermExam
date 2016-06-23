using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASP.NET_FinalTermExam.Models
{
    public class EmployeeService
    {
        /// <summary>
        /// 連線
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }
        /// <summary>
        /// 依照條件取得訂單資料
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployByCondtioin(Models.EmployeeSearchCondition a)
        {

            DataTable dt = new DataTable();
            string sql = @"Select EmployeeID,LastName+'-'+FirstName As EmpName,Title,CONVERT(VARCHAR(10),HireDate,111) As HireDate,Gender,DATEDIFF (YEAR,2016/1/1,BirthDate) AS Ago
					From HR.Employees 
                    Where (EmployeeID=@EmployeeID Or @EmployeeID='')
                    And(LastName+'-'+FirstName Like '%'+@EmpName+'%' Or @EmpName='')
                    And(Title=@Title Or @Title='')
                    And(CONVERT(VARCHAR(10),HireDate,111)  BETWEEN '@FirstDate' AND '@FinalDate' Or @FirstDate='' Or @FinalDate='')";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", a.EmployeeID == null ? string.Empty : a.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@EmpName", a.EmpName == null ? string.Empty : a.EmpName));
                cmd.Parameters.Add(new SqlParameter("@Title", a.Title == null ? string.Empty : a.Title));
                cmd.Parameters.Add(new SqlParameter("@FirstDate", a.FirstDate == null ? string.Empty : a.FirstDate));
                cmd.Parameters.Add(new SqlParameter("@FinalDate", a.FinalDate == null ? string.Empty : a.FinalDate));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }


            return this.MapOrderDataToList(dt);
        }
        private List<Models.Employee> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Employee> result = new List<Employee>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Employee()
                {
                    EmployeeID = (int)row["EmployeeID"],
                    EmpName = row["EmpName"].ToString(),
                    Title = row["Title"].ToString(),
                    HireDate = row["HireDate"].ToString(),
                    Gender = row["Gender"].ToString(),
                    Ago = row["Ago"].ToString()

                });
            }
            return result;
        }

    }
}