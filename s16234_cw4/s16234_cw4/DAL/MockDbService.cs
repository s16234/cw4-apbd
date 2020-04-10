using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s16234_cw4.Models;

namespace s16234_cw4.DAL
{
    public class MockDbService : IDbService
    {
        private static List<Student> _students;

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public Student GetStudentById(string id)
        {
            return _students.Find(x => x.IndexNumber == id);
        }

        public void DeleteStudent(string id)
        {
            _students.Remove(GetStudentById(id));
        }

    }
}
