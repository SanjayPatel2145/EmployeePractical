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
    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Employee obj = new Employee();
                obj.Emp_Name = "";
                obj.Emp_DOB = DateTime.Now; ;
                obj.Emp_Department = "";
                obj.Emp_Status = 1;

                ViewData["Department"] = Constants.BindDepartment();
                ViewData["Status"] = Constants.BindStatus();
                ViewData["Error"] = "";

                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection fr)
        {
            try
            {
                Employee obj = new Employee();
                obj.Emp_Name = Convert.ToString(fr["Emp_Name"]);
                obj.Emp_Department = Convert.ToString(fr["Emp_Department"]);
                obj.Emp_DOB = Convert.ToDateTime(fr["Emp_DOB"]);
                obj.Emp_Status = Convert.ToInt32(fr["Emp_Status"]);

                int CheckCount = obj.check(0, obj.Emp_Name);
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
                        ViewData["Department"] = Constants.BindDepartment();
                        ViewData["Status"] = Constants.BindStatus();

                        return View(obj);
                    }
                }
                else
                {
                    ViewData["Error"] = "Employee already exist !!";
                    ViewData["Department"] = Constants.BindDepartment();
                    ViewData["Status"] = Constants.BindStatus();

                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult List()
        {
            try
            {
                ViewData["Department"] = Constants.BindDepartment();
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            try
            {
                Employee obj = new Employee();
                obj = obj.Find(Id);

                ViewData["Department"] = Constants.BindDepartment();
                ViewData["Status"] = Constants.BindStatus();
                ViewData["Error"] = "";

                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Update(FormCollection fr)
        {
            try
            {
                Employee obj = new Employee();
                obj.Emp_Id = Convert.ToInt32(fr["Emp_Id"]);
                obj.Emp_Name = Convert.ToString(fr["Emp_Name"]);
                obj.Emp_Department = Convert.ToString(fr["Emp_Department"]);
                obj.Emp_DOB = Convert.ToDateTime(fr["Emp_DOB"]);
                obj.Emp_Status = Convert.ToInt32(fr["Emp_Status"]);

                int CheckCount = obj.check(obj.Emp_Id, obj.Emp_Name);
                if (CheckCount == 0)
                {
                    int Result = obj.Update(obj);
                    if (Result > 0)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ViewData["Error"] = "Something was wron‪g !!";
                        ViewData["Department"] = Constants.BindDepartment();
                        ViewData["Status"] = Constants.BindStatus();

                        return View(obj);
                    }
                }
                else
                {
                    ViewData["Error"] = "Employee already exist !!";
                    ViewData["Department"] = Constants.BindDepartment();
                    ViewData["Status"] = Constants.BindStatus();

                    return View(obj);
                }
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
                int Result = Employee.Delete(Id);
                if (Result > 0)
                {
                    ResultData = "Record delete successfully.";
                }
                else if (Result == -1)
                {
                    ResultData = "Already use."; 
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

        
        public string JsonList(string Department)
        {
            try
            {
                JsonResultData obj = new JsonResultData();
                ArrayList list = Employee.List(Department);
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