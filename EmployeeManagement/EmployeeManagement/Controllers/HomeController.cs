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
            Employee emp = _employeeRepository.GetEmployee(id.Value);

            if (emp == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = emp,
                PageTitle = "Employee Details Strongly typed"
            };

            //Employee model = _employeeRepository.GetEmployee(id);

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

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateVM emp)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = new Employee
                {
                    Name = emp.Name,
                    Email = emp.Email,
                    Department = emp.Department
                };
                Employee newEmpl = _employeeRepository.AddEmployee(newEmployee);
                return RedirectToAction("details", new { id = newEmpl.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee existingEmp = _employeeRepository.GetEmployee(id);
            EmployeeEditVM employeeEditVM = new EmployeeEditVM
            {
                Id = existingEmp.Id,
                Name = existingEmp.Name,
                Email = existingEmp.Email,
                Department = existingEmp.Department
            };
            return View(employeeEditVM);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditVM emp)
        {
            if (ModelState.IsValid)
            {
                Employee updateEmployee = new Employee
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Email = emp.Email,
                    Department = emp.Department
                };
                Employee updatedEmp = _employeeRepository.EditEmployee(updateEmployee);
                return RedirectToAction("details", new { id = updatedEmp.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Employee emp = _employeeRepository.GetEmployee(id.Value);

            if (emp == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            else
            {
                _employeeRepository.DeleteEmployee(id.Value);
            }

            return View("delete", id);
        }
    }
}
