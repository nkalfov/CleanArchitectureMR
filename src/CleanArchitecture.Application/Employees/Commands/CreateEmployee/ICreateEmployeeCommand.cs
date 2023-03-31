using System;
using CleanArchitecture.Application.Sales.Commands.CreateSale;

namespace CleanArchitecture.Application.Employees.Commands.CreateEmployee
{
    public interface ICreateEmployeeCommand
    {
        Task ExecuteAsync(CreateSaleModel model);
    }
}

