using CleanArchitecture.Application.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Presentation.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers.Employees
{
    public class EmployeesCreateController : Controller
    {
        private readonly ICreateEmployeeCommand _createEmployeeCommand;

        public EmployeesCreateController(
            ICreateEmployeeCommand createEmployeeCommand)
        {
            _createEmployeeCommand = createEmployeeCommand;
        }

        [HttpGet(CommonRoutes.EmployeesCreate)]
        public IActionResult Create()
        {
            ViewBag.Title = PageTitles.EmployeesCreate;

            return View();
        }

        [HttpPost(CommonRoutes.EmployeesCreate)]
        public async Task<IActionResult> Create(BaseEmployeeModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            await _createEmployeeCommand.ExecuteAsync(model);

            return RedirectToAction("Index", "EmployeesList");
        }
    }
}

