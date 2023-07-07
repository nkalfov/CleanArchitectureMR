using System.Linq.Expressions;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IGetEmployeeByIdQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetEmployeeByIdQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        private readonly Expression<Func<Employee, EmployeeModel>> _projection =
            x => new EmployeeModel
            {
                Id = x.Id,
                Name = x.Name
            };

        public async Task<EmployeeModel?> ExecuteAsync(long id)
        {
            var employees = _databaseService
                .Employees
                .Where(x => x.Id == id)
                .Select(_projection);

            return await employees.SingleOrDefaultAsync();
        }
    }
}

