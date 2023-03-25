using System;
using CleanArchitecture.Application.Sales.Commands.CreateSale.Factory;
using CleanArchitecture.Domain;
using Xunit;

namespace CleanArchitecture.Application.Tests.Sales.Commands.CreateSale
{
    public class SaleFactoryTests
    {
        [Fact(DisplayName = "Create() Creates a Sale")]
        public void CreateASale()
        {
            // Arrange
            var date = DateTimeOffset.Now;

            var customer = new Customer
            {
                Id = 1,
                Name = "John Doe"
            };

            var employee = new Employee
            {
                Id = 1,
                Name =
                "Jahn Doe"
            };

            var product = new Product
            {
                Id = 1,
                Name = "Burzum - Hvis Lyset Tar Oss",
                Price = 9.99m
            };

            var factory = new SaleFactory();

            // Act
            var actual = factory.Create(date, customer, employee, product, 2);


            // Assert
            Assert.NotNull(actual);

            Assert.Equal(date, actual.Date);

            Assert.Same(customer, actual.Customer);
            Assert.Equal(customer.Id, actual.Customer.Id);
            Assert.Equal(customer.Name, actual.Customer.Name);

            Assert.Same(employee, actual.Employee);
            Assert.Equal(employee.Id, actual.Employee.Id);
            Assert.Equal(employee.Name, actual.Employee.Name);

            Assert.Same(product, actual.Product);
            Assert.Equal(product.Id, actual.Product.Id);
            Assert.Equal(product.Name, actual.Product.Name);
            Assert.Equal(product.Price, actual.Product.Price);

            Assert.Equal(product.Price, actual.UnitPrice);
            Assert.Equal(actual.TotalPrice, actual.UnitPrice * actual.Quantity);
        }
    }

}

