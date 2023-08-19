using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Presentation.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers.Employees
{
    public class EmployeesListController : Controller
    {
        private readonly IGetEmployeesListQuery _getEmployeesListQuery;

        public EmployeesListController(
            IGetEmployeesListQuery getEmployeesListQuery)
        {
            _getEmployeesListQuery = getEmployeesListQuery;
        }

        [HttpGet(CommonRoutes.EmployeesList)]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = PageTitles.EmployeesIndex;

            var employees = await _getEmployeesListQuery.ExecuteAsync();

            return View(employees);
        }
    }
}

