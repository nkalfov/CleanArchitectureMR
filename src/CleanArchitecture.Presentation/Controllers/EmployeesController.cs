using CleanArchitecture.Application.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Presentation.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IGetEmployeesListQuery _getEmployeesListQuery;
        private readonly ICreateEmployeeCommand _createEmployeeCommand;

        public EmployeesController(
            IGetEmployeesListQuery getEmployeesListQuery,
            ICreateEmployeeCommand createEmployeeCommand)
        {
            _getEmployeesListQuery = getEmployeesListQuery;
            _createEmployeeCommand = createEmployeeCommand;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = PageTitles.EmployeesIndex;

            var employees = await _getEmployeesListQuery.ExecuteAsync();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = PageTitles.EmployeesCreate;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _createEmployeeCommand.ExecuteAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}