using System;
using CleanArchitecture.Application.Employees.Factories.Contracts;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Employees.Factories
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

