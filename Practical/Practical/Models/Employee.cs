using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace Practical.Models
{
    public class Employee
    {
        [Key]
        public int Emp_Id { get; set; }

        [Display(Name = "Employee Name")]
        [Required]
        [MaxLength(length: 50)]
        public string Emp_Name { get; set; }

        [Display(Name = "Department Name")]
        [Required]
        public string Emp_Department { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Emp_DOB { get; set; }

        [Display(Name = "Status")]
        public int Emp_Status { get; set; }

        public string Age { get; set; }

        public int Create(Employee obj)
        {
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("CreateEmployee", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Emp_Name", obj.Emp_Name);
                cmd.Parameters.AddWithValue("@Emp_Department", obj.Emp_Department);
                cmd.Parameters.AddWithValue("@Emp_DOB", obj.Emp_DOB);
                cmd.Parameters.AddWithValue("@Emp_Status", obj.Emp_Status);
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

        public int check(int Id, String Name)
        {
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("CheckEmployee", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);
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

        public static ArrayList List(string Department)
        {
            ArrayList list = new ArrayList();
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("ListEmployee", sqlcon);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlcon.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Employee obj = new Employee();
                    obj.Emp_Id = Convert.ToInt32(rd["Emp_Id"]);
                    obj.Emp_Name = Convert.ToString(rd["Emp_Name"]);
                    obj.Emp_Department = Convert.ToString(rd["Emp_Department"]);
                    obj.Emp_DOB = Convert.ToDateTime(rd["Emp_DOB"]);
                    obj.Age = Convert.ToString(rd["Age"]);
                    obj.Emp_Status = Convert.ToInt32(rd["Emp_Status"]);
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

        public Employee Find(int Id)
        {
            Employee obj = new Employee();
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("FindEmployee", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                sqlcon.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    obj.Emp_Id = Convert.ToInt32(rd["Emp_Id"]);
                    obj.Emp_Name = Convert.ToString(rd["Emp_Name"]);
                    obj.Emp_Department = Convert.ToString(rd["Emp_Department"]);
                    obj.Emp_DOB = Convert.ToDateTime(rd["Emp_DOB"]);
                    obj.Emp_Status = Convert.ToInt32(rd["Emp_Status"]);
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
            return obj;
        }

        public int Update(Employee obj)
        {
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployee", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Emp_Id", obj.Emp_Id);
                cmd.Parameters.AddWithValue("@Emp_Name", obj.Emp_Name);
                cmd.Parameters.AddWithValue("@Emp_Department", obj.Emp_Department);
                cmd.Parameters.AddWithValue("@Emp_DOB", obj.Emp_DOB);
                cmd.Parameters.AddWithValue("@Emp_Status", obj.Emp_Status);
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

        public static int Delete(int Id)
        {
            SqlConnection sqlcon = new SqlConnection(Constants.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployee", sqlcon);
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