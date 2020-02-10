using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{

    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,
                              IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);

        }

        public IActionResult Details(int? id)
        {
            throw new Exception("Error in Details View");

            Employee employee = _employeeRepository.GetEmployee(id ?? 1);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get Image uploaded path
                string uniqueFileName = ProcessUploadedFile(model.Photo);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction(nameof(Details), new { id = newEmployee.Id });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employeeFromDB = _employeeRepository.GetEmployee(id.Value);
            EmployeeEditViewModel model = new EmployeeEditViewModel
            {
                Id = employeeFromDB.Id,
                Name = employeeFromDB.Name,
                Email = employeeFromDB.Email,
                Department = employeeFromDB.Department,
                ExistingPhotoPath = employeeFromDB.PhotoPath
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {                
                return View(model);
            }

            Employee employeeFromDB = _employeeRepository.GetEmployee(model.Id);
            if (employeeFromDB == null)
            {
                return NotFound();
            }

            // Update retrieved employee
            employeeFromDB.Name = model.Name;
            employeeFromDB.Email = model.Email;
            employeeFromDB.Department = model.Department;
            // Check if photo has changed
            if (model.Photo != null)
            {
                employeeFromDB.PhotoPath = ProcessUploadedFile(model.Photo);
                if(model.ExistingPhotoPath != null)
                {
                    RemoveExitstingPhoto(model.ExistingPhotoPath);
                }
            } 
            // Remove old Photo
            employeeFromDB = _employeeRepository.Update(employeeFromDB);
            return RedirectToAction(nameof(Details), new { id = employeeFromDB.Id });

        }

        [NonAction]
        private string ProcessUploadedFile(IFormFile uploadedFile)
        {
            string uniqueFileName = null;
            if (uploadedFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [NonAction]
        private void RemoveExitstingPhoto(string existingPhoto)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
            string filePath = Path.Combine(uploadsFolder, existingPhoto);
            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
