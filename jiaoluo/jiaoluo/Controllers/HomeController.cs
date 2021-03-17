using jiaoluo.IBLL;
using jiaoluo.Models;
using jiaoluo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jiaoluo.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _istudentRepository;

        public HomeController(IStudentRepository istudentRepository)
        {
            _istudentRepository = istudentRepository;
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
    }
}
