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
                new Student(){Id=1,Name="张三",ClassName=ClassNameEnum.FirstGrade,Email="111@123.com" },
                new Student(){Id=2,Name="张四",ClassName=ClassNameEnum.SecondGrade,Email="221@123.com" },
                new Student(){Id=3,Name="张五",ClassName=ClassNameEnum.GradeThree,Email="233@123.com" },
            };
        }

        public Student Add(Student student)
        {
            student.Id = _students.Max(s => s.Id) + 1;
            _students.Add(student);
            return student;
        }

        public Student Delete(int Id)
        {
            Student student = _students.FirstOrDefault(s => s.Id == Id);
            if (student != null)
            {
                _students.Remove(student);

            }
            return student;
        }

        public string GetAllCount()
        {
            return _students.Count.ToString();
        }

        public Student GetStudent(int id)
        {
            return _students.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public Student Update(Student student)
        {
            Student UpStudent = _students.FirstOrDefault(s => s.Id == student.Id);

            if (UpStudent != null)
            {
                UpStudent.Name = student.Name;
                UpStudent.Email = student.Email;
                UpStudent.ClassName = student.ClassName;
            }

            return UpStudent;
        }
    }
}
