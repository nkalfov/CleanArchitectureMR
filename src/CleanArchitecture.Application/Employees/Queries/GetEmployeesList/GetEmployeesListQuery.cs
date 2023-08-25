using System.Linq.Expressions;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQuery : IGetEmployeesListQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetEmployeesListQuery(IDatabaseService database)
        {
            _databaseService = database;
        }

        private readonly Expression<Func<Employee, EmployeeModel>> _projection =
            x => new EmployeeModel
            {
                Id = x.Id,
                Name = x.Name
            };

        public async Task<IList<EmployeeModel>> ExecuteAsync()
        {
            var employees = _databaseService
                .Employees
                .Select(_projection);

            return await employees.ToListAsync();
        }
    }
}

