using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IGetEmployeesListQuery _getEmployeesListQuery;

        public EmployeesController(
            IGetEmployeesListQuery getEmployeesListQuery)
        {
            _getEmployeesListQuery = getEmployeesListQuery;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Title = "Employees";

            var employees = await _getEmployeesListQuery.ExecuteAsync();

            return View(employees);
        }
    }
}