using System;
using System.Linq.Expressions;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Sales.Queries.GetSaleDetails
{
    public class GetSaleDetailsQuery : IGetSaleDetailsQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetSaleDetailsQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        private static readonly Expression<Func<Sale, SaleDetailsModel>> _projection =
            x => new SaleDetailsModel {
                CustomerName = x.Customer.Name,
                Date = x.Date,
                EmployeeName = x.Employee.Name,
                Id = x.Id,
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice,
                UnitPrice = x.UnitPrice
            };

        public Task<SaleDetailsModel?> ExecuteAsync(long id)
        {
            return _databaseService
                .Sales
                .Where(x => x.Id == id)
                .Select(_projection)
                .SingleOrDefaultAsync();
        }
    }
}

