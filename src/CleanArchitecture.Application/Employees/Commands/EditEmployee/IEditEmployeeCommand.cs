using CleanArchitecture.Application.Employees.ViewModels;

namespace CleanArchitecture.Application.Employees.Commands.EditEmployee
{
    public interface IEditEmployeeCommand
    {
        Task ExecuteAsync(EmployeeModel model);
    }
}

