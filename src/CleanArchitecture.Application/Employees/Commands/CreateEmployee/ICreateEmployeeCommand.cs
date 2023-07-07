using CleanArchitecture.Application.Employees.ViewModels;

namespace CleanArchitecture.Application.Employees.Commands.CreateEmployee
{
    public interface ICreateEmployeeCommand
    {
        Task ExecuteAsync(BaseEmployeeModel model);
    }
}

