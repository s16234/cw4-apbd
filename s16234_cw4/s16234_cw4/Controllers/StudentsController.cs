using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using s16234_cw4.DAL;
using s16234_cw4.Models;

namespace s16234_cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IDbService _dbService;
        private const string ConString = "Data Source=db-mssql16;Initial Catalog=s16234;Integrated Security=True";

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student order by LastName";

                con.Open();
                SqlDataReader sdr = com.ExecuteReader();

                if (sdr.Read())
                {
                    Student s = new Student();
                    s.FirstName = sdr["FirstName"].ToString();
                    s.LastName = sdr["LastName"].ToString();
                    s.BirthDate = sdr["BirthDate"].ToString();
                    s.CourseName = sdr["CourseName"].ToString();
                    s.Semester = Int32.Parse(sdr["Semester"].ToString());
                    s.IndexNumber = sdr["IndexNumber"].ToString();

                    students.Add(s);
                }
            }

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(string id)
        {
            List<Student> students = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student where IndexNumber = " + @id;

                con.Open();
                SqlDataReader sdr = com.ExecuteReader();

                if (sdr.Read())
                {
                    Student s = new Student();
                    s.FirstName = sdr["FirstName"].ToString();
                    s.LastName = sdr["LastName"].ToString();
                    s.BirthDate = sdr["BirthDate"].ToString();
                    s.CourseName = sdr["CourseName"].ToString();
                    s.Semester = Int32.Parse(sdr["Semester"].ToString());
                    s.IndexNumber = sdr["IndexNumber"].ToString();

                    return Ok(s);
                }
                else
                {
                    return NotFound("Student not found");
                }
            } 
        }

        [HttpPost]
        public IActionResult AddStudent(Student s)
        {
            _dbService.AddStudent(s);

            return Ok(s);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(string id)
        {
            Student s = _dbService.GetStudentById(id);

            if(s != null)
            {
                _dbService.DeleteStudent(id);
            }
            

            return Ok("Deleted.");
        }
    }
}