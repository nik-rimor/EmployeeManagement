using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Attribute routing on the method
        [Route("")] //  path is root  '  /  '
        //[Route("Home")] // path is '  /Home  '
        //[Route("Home/Index")]  // path is '  /Home/Index '
        [Route("~/")]
        [Route("~/Home")] // to override controller routing that expects action token
        public ViewResult Index()
        {
            var  model = _employeeRepository.GetAllEmployee();
            return View(model);

        }

        // using token replacement
        //[Route("Home/[action]/{id?}")]
        [Route("{id?}")]
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }
    }
}
