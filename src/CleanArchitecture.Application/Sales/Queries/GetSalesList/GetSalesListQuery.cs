using System;
using System.Linq.Expressions;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Sales;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Sales.Queries.GetSalesList
{
	public class GetSalesListQuery : IGetSalesListQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetSalesListQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        private static readonly Expression<Func<Sale, SalesListItemModel>> _projection =
            x => new SalesListItemModel
            {
                CustomerName = x.Customer.Name,
                Date = x.Date,
                EmployeeName = x.Employee.Name,
                Id = x.Id,
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice
            };

        public async Task<IList<SalesListItemModel>> ExecuteAsync()
        {
            var sales = _databaseService
                .Sales
                .Select(_projection);

            return await sales.ToListAsync();
        }
    }
}

