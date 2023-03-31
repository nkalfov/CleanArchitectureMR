using System;
using CleanArchitecture.Application.Sales.Commands.CreateSale;

namespace CleanArchitecture.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : ICreateEmployeeCommand
    {
        public Task ExecuteAsync(CreateSaleModel model)
        {
            throw new NotImplementedException();
        }
    }
}

