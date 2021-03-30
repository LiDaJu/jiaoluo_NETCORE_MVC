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

        public IActionResult Details(int id)
        {

            Student student = _istudentRepository.GetStudent(id);

            if (student==null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound",id);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                student = student,
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
                    uniqueFileName = UpdatePhotoFile(model);
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

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            Student student = _istudentRepository.GetStudent(Id);
            if (student != null)
            {
                StudentEditViewModel studentEditViewModel = new StudentEditViewModel
                {
                    Id = student.Id,
                    Name = student.Name,
                    ClassName = student.ClassName,
                    Email = student.Email,
                    ExistingPhotoPath = student.PhotoPath
                };
                return View(studentEditViewModel);
            }
            throw new Exception("未查询到学生信息");
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = _istudentRepository.GetStudent(model.Id);
                student.Name = model.Name;
                student.ClassName = model.ClassName;
                student.Email = model.Email;

                if (model.Photos.Count() > 0)
                {
                    if (model.ExistingPhotoPath != null)
                    {

                        //当前图片在项目中的地址
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath + "images", model.ExistingPhotoPath);

                        System.IO.File.Delete(filePath);

                        student.PhotoPath = UpdatePhotoFile(model);

                        Student upDateStudent = _istudentRepository.Update(student);

                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }

        private string UpdatePhotoFile(StudentCreateViewModel model)
        {
            string uniqueFileName = null;
            foreach (var photo in model.Photos)
            {
                //将文件上传到指定文件中；
                //获取指定文件夹路径需要注入HostingEnvironment服务；
                //通过HostingEnvironment服务去获取文件夹路径；
                string uploadsForlder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath1 = Path.Combine(uploadsForlder, uniqueFileName);
                using (var fileStream = new FileStream(filePath1, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
