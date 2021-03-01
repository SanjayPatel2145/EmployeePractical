using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Practical.Models
{
    public class Constants
    {
        internal static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static List<SelectListItem> BindDepartment()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "Account",
                Value = "Account"
            });

            listItems.Add(new SelectListItem
            {
                Text = "Sales",
                Value = "Sales"
            });

            listItems.Add(new SelectListItem
            {
                Text = "Marketing",
                Value = "Marketing"
            });

            listItems.Add(new SelectListItem
            {
                Text = "Purchase",
                Value = "Purchase"
            });

            listItems.Add(new SelectListItem
            {
                Text = "Support",
                Value = "Support"
            });

            listItems.Add(new SelectListItem
            {
                Text = "IT",
                Value = "IT"
            });

            return listItems;
        }

        public static List<SelectListItem> BindStatus()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "Active",
                Value = "1"
            });

            listItems.Add(new SelectListItem
            {
                Text = "In-Active",
                Value = "0"
            });

            return listItems;
        }

        public static List<SelectListItem> BindActiveEmployee()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            ArrayList list = Employee.List("");

            foreach (Employee obj in list)
            {
                if (obj.Emp_Status == 1)
                {
                    listItems.Add(new SelectListItem
                    {
                        Text = obj.Emp_Name,
                        Value = obj.Emp_Id.ToString()
                    });
                }
            }

            return listItems;
        }

        public static List<SelectListItem> BindAllEmployee()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            ArrayList list = Employee.List("");

            foreach (Employee obj in list)
            {
                listItems.Add(new SelectListItem
                {
                    Text = obj.Emp_Name,
                    Value = obj.Emp_Id.ToString()
                });
            }

            return listItems;
        }
    }
}