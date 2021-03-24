using jiaoluo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jiaoluo.IBLL
{
    public interface IStudentRepository
    {
        /// <summary>
        /// 获取指定学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);

        /// <summary>
        /// 获取所有学生信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Student> GetStudents();

        /// <summary>
        /// 添加学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Add(Student student);

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Update(Student student);

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Student Delete(int Id);
    }
}
