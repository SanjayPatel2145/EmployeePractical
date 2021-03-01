using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace Practical.Models
{
    public class EmployeeSalary
    {
        [Key]
        public int Es_Id { get; set; }

        [Display(Name = "Employee Name")]
        [Required]
        public int Es_Emp_Id { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Es_Date { get; set; }

        [Display(Name = "Salary")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Es_Salary { get; set; }

        [Display(Name = "Average")]
        public decimal Average { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Department")]
        public string Department { get; set; }

        public int Create(EmployeeSalary obj)
        {
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("CreateEmployeeSalary", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Es_Emp_Id", obj.Es_Emp_Id);
                cmd.Parameters.AddWithValue("@Es_Date", obj.Es_Date);
                cmd.Parameters.AddWithValue("@Es_Salary", obj.Es_Salary);
                sqlcon.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public int check(int Id, DateTime Date)
        {
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("CheckEmployeeSalary", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Date", Date);
                sqlcon.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public static ArrayList List(string FromDate, string ToDate, int EmpId, string Department)
        {
            ArrayList list = new ArrayList();
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("ListEmployeeSalary", sqlcon);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlcon.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    EmployeeSalary obj = new EmployeeSalary();
                    obj.Es_Emp_Id = Convert.ToInt32(rd["Emp_Id"]);
                    obj.EmployeeName = Convert.ToString(rd["Emp_Name"]);
                    obj.Department = Convert.ToString(rd["Emp_Department"]);
                    obj.Average = Convert.ToDecimal(rd["Average"]);

                    list.Add(obj);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
            return list;
        }

        public static ArrayList Detail(string FromDate, string ToDate, int EmpId)
        {
            ArrayList list = new ArrayList();
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("EmployeeSalaryDetail", sqlcon);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlcon.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    EmployeeSalary obj = new EmployeeSalary();
                    obj.Es_Id = Convert.ToInt32(rd["Es_Id"]);
                    obj.Es_Emp_Id = Convert.ToInt32(rd["Es_Emp_Id"]);
                    obj.Es_Salary = Convert.ToDecimal(rd["Es_Salary"]);
                    obj.Es_Date = Convert.ToDateTime(rd["Es_Date"]);
                    list.Add(obj);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
            return list;
        }

        public static int Delete(int Id)
        {
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployeeSalary", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                sqlcon.Open();
                return Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }
    }
}