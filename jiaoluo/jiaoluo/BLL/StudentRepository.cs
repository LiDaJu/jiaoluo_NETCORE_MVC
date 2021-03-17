using jiaoluo.IBLL;
using jiaoluo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jiaoluo.BLL
{
    public class StudentRepository : IStudentRepository
    {
        private List<Student> _students;

        public StudentRepository()
        {
            _students = new List<Student>()
            {
                new Student(){Id=1,Name="张三",ClassName="一",Email="111@123.com" },
                new Student(){Id=2,Name="张四",ClassName="二",Email="221@123.com" },
                new Student(){Id=3,Name="张五",ClassName="三",Email="233@123.com" },
            };
        }

        public Student GetStudent(int id)
        {
            return _students.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
