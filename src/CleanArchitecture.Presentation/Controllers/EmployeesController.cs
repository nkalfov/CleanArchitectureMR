using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Presentation.Resources;
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
    }
}