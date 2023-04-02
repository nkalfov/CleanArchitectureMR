using System;
using CleanArchitecture.Application.Employees.Commands.CreateEmployee;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Employees.Factories.Contracts
{
    public interface IEmployeeFactory
    {
        Employee Create(CreateEmployeeModel model);
    }
}

