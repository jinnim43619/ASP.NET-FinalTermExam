using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_FinalTermExam.Controllers
{
    public class EmployeeController : Controller
    {
        Models.GetInfo GetInfo = new Models.GetInfo();
        Models.EmployeeService EmployeeService = new Models.EmployeeService();
        // GET: Employee
        /// <summary>
        /// 員工頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.TitleData = this.GetInfo.GetTitle();
            return View();
        }
        /// <summary>
        /// 取得員工查詢
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.EmployeeSearchCondition a)
        {
            ViewBag.TitleData = this.GetInfo.GetTitle();
            ViewBag.Employee = EmployeeService.GetEmployByCondtioin(a);
            return View("Index");
        }
    }
}