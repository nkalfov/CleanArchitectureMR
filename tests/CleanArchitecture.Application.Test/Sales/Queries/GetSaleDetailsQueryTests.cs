using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Sales.Queries.GetSaleDetails;
using CleanArchitecture.Domain;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace CleanArchitecture.Application.Tests.Sales.Queries
{
    public class GetSaleDetailsQueryTests
    {
        private readonly SaleDetailsModel _expected;
        private readonly GetSaleDetailsQuery _query;

        public GetSaleDetailsQueryTests()
        {
            _expected = new SaleDetailsModel
            {
                Id = 1,
                Date = DateTimeOffset.Now,
                CustomerName = "Deckard Cain",
                EmployeeName = "Tyreal",
                ProductName = "The Horadic Qube",
                Quantity = 1,
                TotalPrice = 12.34m,
                UnitPrice = 12.34m
            };

            var sale = new Sale
            {
                Date = _expected.Date,
                Customer = new Customer
                {
                    Id = 1,
                    Name = _expected.CustomerName
                },
                Employee = new Employee
                {
                    Id = 1,
                    Name = _expected.EmployeeName
                },
                Id = _expected.Id,
                Product = new Product
                {
                    Id = 1,
                    Name = _expected.ProductName
                },
                Quantity = _expected.Quantity,
                UnitPrice = _expected.UnitPrice
            };

            var databaseMock = new Mock<IDatabaseService>();            
            databaseMock
                .Setup(x => x.Sales)
                .ReturnsDbSet(new List<Sale> { sale });

            _query = new GetSaleDetailsQuery(databaseMock.Object);
        }


        [Fact(DisplayName = "Get Existing Sale Details")]
        public void GetSaleDetails()
        {
            // Act
            var actual = _query.ExecuteAsync(_expected.Id).GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(_expected.Id, actual.Id);
            Assert.Equal(_expected.CustomerName, actual.CustomerName);
            Assert.Equal(_expected.Date, actual.Date);
            Assert.Equal(_expected.EmployeeName, actual.EmployeeName);
            Assert.Equal(_expected.ProductName, actual.ProductName);
            Assert.Equal(_expected.Quantity, actual.Quantity);
            Assert.Equal(_expected.TotalPrice, actual.TotalPrice);
            Assert.Equal(_expected.UnitPrice, actual.UnitPrice);
        }


        [Fact(DisplayName = "Get Ussnexisting Sale Details")]
        public void GetUnexistingSaleDetails()
        {
            // Act
            var actual = _query.ExecuteAsync(10).GetAwaiter().GetResult();

            // Assert
            Assert.Null(actual);
        }
    }
}