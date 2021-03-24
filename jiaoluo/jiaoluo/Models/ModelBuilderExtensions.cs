using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jiaoluo.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                 new Student
                 {
                     Id = 1,
                     Name = "张三",
                     ClassName = ClassNameEnum.FirstGrade,
                     Email = "111@123.com"
                 },
                 new Student
                 {
                     Id = 2,
                     Name = "张四",
                     ClassName = ClassNameEnum.SecondGrade,
                     Email = "221@123.com"
                 },
                 new Student
                 {
                     Id = 3,
                     Name = "张五",
                     ClassName = ClassNameEnum.GradeThree,
                     Email = "233@123.com"
                 });
        }
    }
}
