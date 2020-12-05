using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller //controller is base class for all controller in MVC
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //[Route("~/Home")]
        //[Route("/")]
        public ViewResult Index()
        {
           return View(_employeeRepository.GetAllEmployee());
        }

        //[Route("{id?}")]
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details Strongly typed"
            };

            Employee model = _employeeRepository.GetEmployee(1);

            //in View data we create string variable
            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details View Data";

            //in View data we create  objects
            //ViewBag.Employee = model;
            //ViewBag.PageTitle = "Employee Details View Bag";

            //ViewBag.PageTitle = "Employee Details Strongly typed";
            return View(homeDetailsViewModel);
            //return View("Test");//use this we not want to use the action name, extension is not required
            //return View("MyViews/Test.cshtml");//use this when views are not in controller named folder,extension is required
        }

        public ViewResult Create()
        {
            return View();
        }
    }
}
