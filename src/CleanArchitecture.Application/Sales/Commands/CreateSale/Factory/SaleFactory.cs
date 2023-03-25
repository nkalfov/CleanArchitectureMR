using System;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Sales.Commands.CreateSale.Factory
{
    public class SaleFactory : ISaleFactory
    {
        public Sale Create(
            DateTimeOffset date,
            Customer customer,
            Employee employee,
            Product product,
            int quantity)
        {
            var sale = new Sale
            {
                Date = date,
                Customer = customer,
                Employee = employee,
                Product = product,
                UnitPrice = product.Price,
                Quantity = quantity
            };

            return sale;
        }
    }
}

