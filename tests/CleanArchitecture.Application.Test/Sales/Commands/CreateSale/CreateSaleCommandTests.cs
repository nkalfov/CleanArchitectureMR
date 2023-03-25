using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Sales.Commands.CreateSale;
using CleanArchitecture.Application.Sales.Commands.CreateSale.Factory;
using CleanArchitecture.Common.Services.Contracts;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace CleanArchitecture.Application.Tests.Sales.Commands.CreateSale
{
    public class CreateSaleCommandTests
    {
        [Fact(DisplayName = "ExecuteAsync() saves a sale into the Database Context")]
        public void CreatingASale()
        {
            // Arrange
            var quantity = 2;

            var date = DateTimeOffset.Now;

            var dateServiceMock = new Mock<IDateService>();
            dateServiceMock
                .Setup(x => x.GetDate())
                .Returns(date);


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

            var databaseServiceMock = new Mock<IDatabaseService>();

            databaseServiceMock
                .Setup(x => x.Customers)
                .ReturnsDbSet(new List<Customer> { customer });

            databaseServiceMock
                .Setup(x => x.Employees)
                .ReturnsDbSet(new List<Employee> { employee });

            databaseServiceMock
                .Setup(x => x.Products)
                .ReturnsDbSet(new List<Product> { product });

            var salesList = new List<Sale>();
            databaseServiceMock
                .Setup(x => x.Sales)
                .ReturnsDbSet(salesList);

            databaseServiceMock
                .Setup(x => x.Sales.AddAsync(
                    It.IsAny<Sale>(),
                    default))
                .Callback(
                    (Sale entity, CancellationToken token) =>
                    {
                        entity.Id = salesList.Count + 1;
                        salesList.Add(entity);
                    })
                .Returns(
                    (Sale entity, CancellationToken token) =>
                    {
                        return ValueTask.FromResult(default(EntityEntry<Sale>));
                    });

            databaseServiceMock
                .Setup(x => x.SaveAsync(default))
                .Returns(Task.CompletedTask);

            var inventoryServiceMock = new Mock<IInventoryService>();
            inventoryServiceMock
                .Setup(x => x.NotifySaleOccurredAsync(
                    It.IsAny<long>(),
                    It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            var sale = new Sale
            {
                Id = 0, // not saved yet!
                Date = date,
                Customer = customer,
                Employee = employee,
                Product = product,
                Quantity = quantity,
                UnitPrice = product.Price
            };

            var saleFactoryMock = new Mock<ISaleFactory>();
            saleFactoryMock
                .Setup(x => x.Create(
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<Customer>(),
                    It.IsAny<Employee>(),
                    It.IsAny<Product>(),
                    It.IsAny<int>()))
                .Returns(sale);

            var command = new CreateSaleCommand(
                dateServiceMock.Object,
                databaseServiceMock.Object,
                inventoryServiceMock.Object,
                saleFactoryMock.Object);

            var createSaleModel = new CreateSaleModel
            {
                CustomerId = customer.Id,
                EmployeeId = employee.Id,
                ProductId = product.Id,
                Quantity = quantity
            };

            // Act
            command.ExecuteAsync(createSaleModel).GetAwaiter().GetResult();

            // Assert
            dateServiceMock.Verify(
                x => x.GetDate(),
                Times.Once);

            databaseServiceMock.Verify(
                x => x.Sales.AddAsync(It.IsAny<Sale>(), default),
                Times.Once);

            databaseServiceMock.Verify(
                x => x.SaveAsync(default),
                Times.Once);

            inventoryServiceMock.Verify(
                x => x.NotifySaleOccurredAsync(It.IsAny<long>(), It.IsAny<int>()),
                Times.Once);
        }
    }
}

