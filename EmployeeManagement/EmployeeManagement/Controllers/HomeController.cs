using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller //controller is base class for all controller in MVC
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public string Index()
        {
           return _employeeRepository.GetEmployee(1).Name;
        }

        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);

            ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details View Data";

            ViewBag.Employee = model;
            //ViewBag.PageTitle = "Employee Details View Bag";

            ViewBag.PageTitle = "Employee Details Strongly typed";
            return View(model);
            //return View("Test");//use this we not want to use the action name, extension is not required
            //return View("MyViews/Test.cshtml");//use this when views are not in controller named folder,extension is required
        }
    }
}
