using jiaoluo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jiaoluo.ViewModel
{
    public class StudentCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "请输入名字"), MaxLength(50, ErrorMessage = "名字的长度不能超出50")]
        public string Name { get; set; }

        [Display(Name = "班级")]
        [Required(ErrorMessage = "请选择班级")]
        public ClassNameEnum? ClassName { get; set; }

        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        [Display(Name = "图片")]
        public List<IFormFile> Photos { get; set; }
    }
}
