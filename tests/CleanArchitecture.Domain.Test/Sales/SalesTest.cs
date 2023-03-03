using System;
using Xunit;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Domain.Test.Sales
{
    public class SalesTest
    {
        [Theory(DisplayName = "Initializing Sales")]
        [InlineData(1, 100, 5)]
        [InlineData(1, 777.77, 1)]
        [InlineData(1, 99.99, 3)]
        [InlineData(1, 12.54, 8)]
        public void InitializingSales(
            long id,
            decimal unitPrice,
            int quantity)
        {
            var date = DateTimeOffset.Now;
            var customer = new Customer { Id = id };
            var employee = new Employee { Id = id };
            var product = new Product { Id = id, Price = unitPrice };
            var totalPrice = unitPrice * quantity;

            var actual = new Sale
            {
                Id = id,
                UnitPrice = unitPrice,
                Quantity = quantity,
                Date = date,
                Customer = customer,
                Employee = employee,
                Product = product
            };

            Assert.Equal(id, actual.Id);
            Assert.Equal(unitPrice, actual.UnitPrice);
            Assert.Equal(quantity, actual.Quantity);
            Assert.Equal(totalPrice, actual.TotalPrice);
            Assert.Equal(date, actual.Date);
            Assert.Equal(customer, actual.Customer);
            Assert.Equal(employee, actual.Employee);
            Assert.Equal(product, actual.Product);
        }
    }
}