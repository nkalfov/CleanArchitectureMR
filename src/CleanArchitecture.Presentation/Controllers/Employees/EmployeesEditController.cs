using System;
using CleanArchitecture.Application.Employees.Commands.EditEmployee;
using CleanArchitecture.Application.Employees.Queries.GetEmployeeById;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Presentation.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers.Employees
{
    public class EmployeesEditController : Controller
    {
        private readonly IGetEmployeeByIdQuery _getEmployeeByIdQuery;
        private readonly IEditEmployeeCommand _editEmployeeCommand;

        public EmployeesEditController(
            IGetEmployeeByIdQuery getEmployeeByIdQuery,
            IEditEmployeeCommand editEmployeeCommand)
        {
            _getEmployeeByIdQuery = getEmployeeByIdQuery;
            _editEmployeeCommand = editEmployeeCommand;
        }

        [HttpGet(CommonRoutes.EmployeesEdit)]
        public async Task<IActionResult> Edit(long id)
        {
            var model = await _getEmployeeByIdQuery.ExecuteAsync(id);

            if (model is null)
                return NotFound();

            return View(model);
        }

        [HttpPost(CommonRoutes.EmployeesEdit)]
        public async Task<IActionResult> Edit(EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _editEmployeeCommand.ExecuteAsync(model);

            return RedirectToAction("Index", "EmployeesList");
        }
    }
}

