using CleanArchitecture.Application.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Employees.Commands.EditEmployee;
using CleanArchitecture.Application.Employees.Queries.GetEmployeeById;
using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Presentation.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IGetEmployeesListQuery _getEmployeesListQuery;
        private readonly IGetEmployeeByIdQuery _getEmployeeByIdQuery;
        private readonly ICreateEmployeeCommand _createEmployeeCommand;
        private readonly IEditEmployeeCommand _editEmployeeCommand;

        public EmployeesController(
            IGetEmployeesListQuery getEmployeesListQuery,
            IGetEmployeeByIdQuery getEmployeeByIdQuery,
            ICreateEmployeeCommand createEmployeeCommand,
            IEditEmployeeCommand editEmployeeCommand)
        {
            _getEmployeesListQuery = getEmployeesListQuery;
            _getEmployeeByIdQuery = getEmployeeByIdQuery;
            _createEmployeeCommand = createEmployeeCommand;
            _editEmployeeCommand = editEmployeeCommand;
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
        public async Task<IActionResult> Create(BaseEmployeeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _createEmployeeCommand.ExecuteAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var model = await _getEmployeeByIdQuery.ExecuteAsync(id);

            if (model is null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _editEmployeeCommand.ExecuteAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}