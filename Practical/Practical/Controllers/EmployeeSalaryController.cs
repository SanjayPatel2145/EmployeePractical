using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Practical.Models;

namespace Practical.Controllers
{
    public class EmployeeSalaryController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            EmployeeSalary obj = new EmployeeSalary();
            obj.Es_Date = DateTime.Now;
            obj.Es_Id = 0;
            obj.Es_Salary = 0;
            obj.Es_Emp_Id = 0;

            ViewData["Employee"] = Constants.BindActiveEmployee();
            ViewData["Error"] = "";

            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(FormCollection fr)
        {
            EmployeeSalary obj = new EmployeeSalary();
            obj.Es_Date = Convert.ToDateTime(fr["Es_Date"]);
            obj.Es_Salary = Convert.ToDecimal(fr["Es_Salary"]);
            obj.Es_Emp_Id = Convert.ToInt32(fr["Es_Emp_Id"]);

            int CheckCount = obj.check(obj.Es_Emp_Id, obj.Es_Date);
            if (CheckCount == 0)
            {
                int Result = obj.Create(obj);
                if (Result > 0)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewData["Error"] = "Something was wron‪g !!";
                    ViewData["Employee"] = Constants.BindActiveEmployee();

                    return View(obj);
                }
            }
            else
            {
                ViewData["Error"] = "Enter Duplicate Record !!";
                ViewData["Employee"] = Constants.BindActiveEmployee();

                return View(obj);
            }
        }

        public ActionResult List()
        {
            try
            {
                ViewData["Employee"] = Constants.BindAllEmployee();
                ViewData["Department"] = Constants.BindDepartment();
                
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Detail(int Id,string Department,string EmployeeName,string FromDate,string ToDate)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string Delete(int Id)
        {
            try
            {
                string ResultData;
                int Result = EmployeeSalary.Delete(Id);
                if (Result > 0)
                {
                    ResultData = "Record delete successfully.";
                }
                else
                {
                    ResultData = "Something was wron‪g !!";
                }
                return ResultData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string JsonList(int? Employee, string Department, string FromDate, string ToDate)
        {
            try
            {
                JsonResultData obj = new JsonResultData();
                ArrayList list = EmployeeSalary.List((FromDate == null ? "" : FromDate), (ToDate == null ? "" : ToDate), Convert.ToInt32((Employee == null ? 0 : Employee)), (Department == null ? "" : Department));
                obj.Data = list;
                obj.Count = list.Count;
                string JSONString = JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                return JSONString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string JsonListDetail(int Id, string FromDate, string ToDate)
        {
            try
            {
                JsonResultData obj = new JsonResultData();
                ArrayList list = EmployeeSalary.Detail(FromDate, ToDate, Id);
                obj.Data = list;
                obj.Count = list.Count;
                string JSONString = JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                return JSONString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}