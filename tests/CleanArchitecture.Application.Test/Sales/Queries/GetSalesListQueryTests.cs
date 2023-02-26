using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Sales.Queries.GetSalesList;
using CleanArchitecture.Domain.Sales;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace CleanArchitecture.Application.Tests.Sales.Queries
{
    public class GetSalesListQueryTests
    {
        [Fact(DisplayName = "Get All Three Sales")]
        public void QueryThreeSales()
        {
            // Arrange
            var expected = new List<SalesListItemModel>
            {
                new SalesListItemModel
                {
                    Id = 1,
                    CustomerName = "John Doe",
                    Date = DateTimeOffset.Now.AddDays(-5),
                    EmployeeName = "Anders Hejlsberg",
                    ProductName = "Doombringer (Champion Sword)",
                    Quantity = 3,
                    UnitPrice = 5999.99m,
                    TotalPrice = 3m * 5999.99m
                },
                new SalesListItemModel
                {
                    Id = 2,
                    CustomerName = "Dennis Ritchie",
                    Date = DateTimeOffset.Now.AddDays(-3),
                    EmployeeName = "Mads Torgensen",
                    ProductName = "Flamebellow (Balrog Blade)",
                    Quantity = 2,
                    UnitPrice = 7459.55m,
                    TotalPrice = 2m * 7459.55m
                },
                new SalesListItemModel
                {
                    Id = 3,
                    CustomerName = "Bjarne Stroustrup",
                    Date = DateTimeOffset.Now.AddDays(-5),
                    EmployeeName = "James Gosling",
                    ProductName = "The Grandfather (Colossus Blade)",
                    Quantity = 1,
                    UnitPrice = 9999.98m,
                    TotalPrice = 1m * 9999.98m
                }
            };

            var sales = new List<Sale>();
            foreach (var item in expected)
            {
                sales.Add(new Sale
                {
                    Id = item.Id,
                    Customer = new Domain.Customers.Customer
                    {
                        Id = item.Id,
                        Name = item.CustomerName
                    },
                    Date = item.Date,
                    Employee = new Domain.Employees.Employee
                    {
                        Id = item.Id,
                        Name = item.EmployeeName
                    },
                    Product = new Domain.Products.Product
                    {
                        Id = item.Id,
                        Name = item.ProductName,
                        Price = item.UnitPrice
                    },
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var databaseMock = new Mock<IDatabaseService>();
            databaseMock
                .Setup(x => x.Sales)
                .ReturnsDbSet(sales);

            var query = new GetSalesListQuery(databaseMock.Object);

            // Act
            var actual = query.ExecuteAsync().GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int index = 0; index < expected.Count; index++)
            {
                Assert.Equal(expected[index].Id, actual[index].Id);
                Assert.Equal(expected[index].CustomerName, actual[index].CustomerName);
                Assert.Equal(expected[index].Date, actual[index].Date);
                Assert.Equal(expected[index].EmployeeName, actual[index].EmployeeName);
                Assert.Equal(expected[index].ProductName, actual[index].ProductName);
                Assert.Equal(expected[index].Quantity, actual[index].Quantity);
                Assert.Equal(expected[index].TotalPrice, actual[index].TotalPrice);
                Assert.Equal(expected[index].UnitPrice, actual[index].UnitPrice);
            }
        }

        [Fact(DisplayName = "Query Sales on an empty database")]
        public void EmptyDatabase()
        {
            var expected = new List<SalesListItemModel>();

            var sales = new List<Sale>();
            foreach (var item in expected)
            {
                sales.Add(new Sale
                {
                    Id = item.Id,
                    Customer = new Domain.Customers.Customer
                    {
                        Id = item.Id,
                        Name = item.CustomerName
                    },
                    Date = item.Date,
                    Employee = new Domain.Employees.Employee
                    {
                        Id = item.Id,
                        Name = item.EmployeeName
                    },
                    Product = new Domain.Products.Product
                    {
                        Id = item.Id,
                        Name = item.ProductName,
                        Price = item.UnitPrice
                    },
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var databaseMock = new Mock<IDatabaseService>();
            databaseMock
                .Setup(x => x.Sales)
                .ReturnsDbSet(sales);

            var query = new GetSalesListQuery(databaseMock.Object);

            // Act
            var actual = query.ExecuteAsync().GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int index = 0; index < expected.Count; index++)
            {
                Assert.Equal(expected[index].Id, actual[index].Id);
                Assert.Equal(expected[index].CustomerName, actual[index].CustomerName);
                Assert.Equal(expected[index].Date, actual[index].Date);
                Assert.Equal(expected[index].EmployeeName, actual[index].EmployeeName);
                Assert.Equal(expected[index].ProductName, actual[index].ProductName);
                Assert.Equal(expected[index].Quantity, actual[index].Quantity);
                Assert.Equal(expected[index].TotalPrice, actual[index].TotalPrice);
                Assert.Equal(expected[index].UnitPrice, actual[index].UnitPrice);
            }
        }
    }
}

