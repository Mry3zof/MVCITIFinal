using Microsoft.AspNetCore.Mvc;
using ProjectName.BLL.Helpers;
using ProjectName.BLL.Interfaces;
using ProjectName.DAL.Entities;
using ProjectName.Models;

namespace ProjectName.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeFormViewModel model)
        {
            string imagePath = "/images/default.png";

            if (model.Image != null && model.Image.Length > 0)
            {
                imagePath = DocumentSettings.UploadFile(model.Image, "images");
            }

            var employee = new Employee
            {
                Name = model.Name,
                Age = model.Age,
                Salary = model.Salary,
                ImagePath = imagePath,
                IsApproved = true
            };

            _repo.Add(employee);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SaveToSession(int id)
        {
            var emp = _repo.GetById(id);
            if (emp != null)
            {
                HttpContext.Session.SetObject($"employee-details-{id}", emp);
            }
            return RedirectToAction("SessionDetails", new { id });
        }

        public IActionResult SessionDetails(int id)
        {
            var emp = HttpContext.Session.GetObject<Employee>($"employee-details-{id}");
            if (emp == null)
            {
                ViewBag.Message = "No employee stored in session for this ID.";
                return View();
            }
            return View(emp);
        }
    }
}