using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s16234_cw4.Models;

namespace s16234_cw4.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public void AddStudent(Student student);
        public Student GetStudentById(string id);
        public void DeleteStudent(string id);
    }
}
