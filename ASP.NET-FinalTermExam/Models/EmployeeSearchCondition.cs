using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_FinalTermExam.Models
{
    public class EmployeeSearchCondition
    {
        public string EmployeeID { get; set; }
        public string EmpName { get; set; }
        public string Title { get; set; }
        public string FirstDate { get; set; }
        public string FinalDate { get; set; }
    }
}