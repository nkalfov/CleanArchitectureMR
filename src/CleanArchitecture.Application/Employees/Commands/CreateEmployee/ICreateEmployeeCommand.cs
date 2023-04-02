using System;

namespace CleanArchitecture.Application.Employees.Commands.CreateEmployee
{
    public interface ICreateEmployeeCommand
    {
        Task ExecuteAsync(CreateEmployeeModel model);
    }
}

