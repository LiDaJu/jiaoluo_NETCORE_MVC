using jiaoluo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jiaoluo.IBLL
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);

        IEnumerable<Student> GetStudents();

        Student Add(Student student);

        string GetAllCount();
    }
}
