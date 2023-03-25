using System;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Sales.Commands.CreateSale.Factory;
using CleanArchitecture.Common.Services.Contracts;

namespace CleanArchitecture.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommand : ICreateSaleCommand
    {
        private readonly IDateService _dateService;
        private readonly IDatabaseService _databaseService;
        private readonly IInventoryService _inventoryService;
        private readonly ISaleFactory _saleFactory;
        
        public CreateSaleCommand(
            IDateService dateService,
            IDatabaseService databaseService,
            IInventoryService inventoryService,
            ISaleFactory saleFactory)
        {
            _dateService = dateService;
            _databaseService = databaseService;
            _inventoryService = inventoryService;
            _saleFactory = saleFactory;
        }

        public async Task ExecuteAsync(CreateSaleModel model)
        {
            var customer = _databaseService
                .Customers
                .Single(x => x.Id == model.CustomerId);

            var employee = _databaseService
                .Employees
                .Single(x => x.Id == model.EmployeeId);

            var product = _databaseService
                .Products
                .Single(x => x.Id == model.ProductId);

            var sale = _saleFactory.Create(
                _dateService.GetDate(),
                customer,
                employee,
                product,
                model.Quantity);

            await _databaseService.Sales.AddAsync(sale);
            await _databaseService.SaveAsync();

            await _inventoryService.NotifySaleOccurredAsync(model.ProductId, model.Quantity);
        }
    }
}

