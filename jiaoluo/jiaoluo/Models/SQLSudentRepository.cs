using jiaoluo.IBLL;
using System;
using System.Collections.Generic;

namespace jiaoluo.Models
{
    public class SQLSudentRepository : IStudentRepository
    {
        private readonly AppDBContext _appDBContext;

        public SQLSudentRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public Student Add(Student student)
        {
            _appDBContext.Students.Add(student);
            _appDBContext.SaveChanges();
            return student;
        }

        public Student Delete(int Id)
        {
            Student student = _appDBContext.Students.Find(Id);

            if (student != null)
            {
                _appDBContext.Students.Remove(student);
                _appDBContext.SaveChanges();
            }
            return student;
        }

        public Student GetStudent(int id)
        {
            Student student = _appDBContext.Students.Find(id);
            return student;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _appDBContext.Students;
        }

        public Student Update(Student student)
        {
            var student1 = _appDBContext.Students.Attach(student);

            student1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _appDBContext.SaveChanges();
            return student;
        }
    }
}
