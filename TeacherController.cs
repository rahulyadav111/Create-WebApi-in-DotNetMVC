using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using CrudWebAPIMVC.Models;

namespace CrudWebAPIMVC.Controllers
{

    public class TeacherController : ApiController
    {
        string connString = "Data Source=.;Initial Catalog = Rahul; Persist Security Info=True;User ID = sa; Password=Sa@2014";
        List<TeacherModel> Teacher = new List<TeacherModel>();

        [System.Web.Http.Route("api/GetAllTeacher")]
        public IHttpActionResult GetAllTeacher()
        {
           
            string query = "select * from teacher";
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Teacher.Add(new TeacherModel
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Teacher_Name = Convert.ToString(sdr["Teacher_Name"]),
                                Teacher_Email = Convert.ToString(sdr["Teacher_Email"]),
                                Teacher_ContactNo = Convert.ToString(sdr["Teacher_ContactNo"]),
                                Teacher_Address = Convert.ToString(sdr["Teacher_Address"]),
                                Teacher_Department = Convert.ToString(sdr["Teacher_Department"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Ok(Teacher);

        }
        [System.Web.Http.Route("api/GetTeacherById/{id}")]
        public  IHttpActionResult GetTeacherById(int id)
        {
            TeacherModel teacher = new TeacherModel();
           
            string query = "SELECT * FROM Teacher where Id=" + id;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            teacher = new TeacherModel
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Teacher_Name = Convert.ToString(sdr["Teacher_Name"]),
                                Teacher_Email = Convert.ToString(sdr["Teacher_Email"]),
                                Teacher_ContactNo = Convert.ToString(sdr["Teacher_ContactNo"]),
                                Teacher_Address = Convert.ToString(sdr["Teacher_Address"]),
                                Teacher_Department = Convert.ToString(sdr["Teacher_Department"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (teacher.Id == 0 )
            {
                return NotFound();
            }
            return Ok(teacher);

        }
        [System.Web.Http.Route("api/ModifyTeacher/{id}")]
        [System.Web.Http.HttpPut]
        public IHttpActionResult ModifyTeacher(int id, TeacherModel teacher)
        {
            if(id!=teacher.Id)
            {
                return BadRequest();
            }
            TeacherModel teacherModel = new TeacherModel();
            if (ModelState.IsValid)
            {
                string query = "UPDATE Teacher SET Teacher_Name = @Teacher_Name, Teacher_Email = @Teacher_Email," +
                   "Teacher_ContactNo=@Teacher_ContactNo," +
                   "Teacher_Address=@Teacher_Address,Teacher_Department=@Teacher_Department Where Id =@Id";

                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Teacher_Name", teacherModel.Teacher_Name);
                        cmd.Parameters.AddWithValue("@Teacher_Email", teacherModel.Teacher_Email);
                        cmd.Parameters.AddWithValue("@Teacher_ContactNo", teacherModel.Teacher_ContactNo);
                        cmd.Parameters.AddWithValue("@Teacher_Address", teacherModel.Teacher_Address);
                        cmd.Parameters.AddWithValue("@Teacher_Department", teacherModel.Teacher_Department);
                        cmd.Parameters.AddWithValue("@Id", teacherModel.Id);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            return Ok(teacherModel);
                        }
                        con.Close();
                    }
                }

            }
            return BadRequest();

        }

    }
}
   