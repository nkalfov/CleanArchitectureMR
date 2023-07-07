using CleanArchitecture.Application.Employees.ViewModels;

namespace CleanArchitecture.Application.Employees.Queries.GetEmployeeById
{
    public interface IGetEmployeeByIdQuery
    {
        Task<EmployeeModel?> ExecuteAsync(long id);
    }
}

