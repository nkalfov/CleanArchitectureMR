using System.Linq.Expressions;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQuery : IGetEmployeesListQuery
    {
        private readonly IDatabaseService _database;

        public GetEmployeesListQuery(
            IDatabaseService database)
        {
            _database = database;
        }

        private static readonly Expression<Func<Employee, EmployeeModel>> _projection =
            x => new EmployeeModel
            {
                Id = x.Id,
                Name = x.Name
            };

        public async Task<IList<EmployeeModel>> ExecuteAsync()
        {
            var employees = _database
                .Employees
                .Select(_projection);

            return await employees.ToListAsync();
        }
    }
}

