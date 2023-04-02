using System;
using CleanArchitecture.Application.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Employees.Factories.Contracts;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Employees.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        public Employee Create(CreateEmployeeModel model)
        {
            var result = new Employee
            {
                Name = model.Name
            };

            return result;
        }
    }
}

