using System;
namespace CleanArchitecture.Application.Sales.Commands.CreateSale
{
    public interface ICreateSaleCommand
    {
        Task ExecuteAsync(CreateSaleModel model);
    }
}

