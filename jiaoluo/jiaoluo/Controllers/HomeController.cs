using jiaoluo.IBLL;
using jiaoluo.Models;
using jiaoluo.ViewModel;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jiaoluo.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _istudentRepository;
        private readonly HostingEnvironment _hostingEnvironment;

        public HomeController(IStudentRepository istudentRepository, HostingEnvironment hostingEnvironment)
        {
            _istudentRepository = istudentRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> list = _istudentRepository.GetStudents();
            return View(list);
        }

        [Route("{id?}")]
        public IActionResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                student = _istudentRepository.GetStudent(id ?? 1),
                PageTitle = "Student Details"
            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (var photo in model.Photos)
                    {
                        //将文件上传到指定文件中；
                        //获取指定文件夹路径需要注入HostingEnvironment服务；
                        //通过HostingEnvironment服务去获取文件夹路径；
                        string uploadsForlder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsForlder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                Student student = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName

                };
                _istudentRepository.Add(student);

                //跳转到学生详情页面
                return RedirectToAction("Details", new { id = student.Id });
            }
            return View();
        }
    }
}
