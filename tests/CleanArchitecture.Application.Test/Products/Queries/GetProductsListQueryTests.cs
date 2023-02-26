using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Customers.Queries;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Products.Queries.GetProductList;
using CleanArchitecture.Domain.Customers;
using CleanArchitecture.Domain.Products;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace CleanArchitecture.Application.Test.Products.Queries
{
    public class GetProductsListQueryTests
    {
        [Fact(DisplayName = "Get All Three Products")]
        public void QueryThreeProducts()
        {
            // Arrange
            var expected = new List<ProductModel>
            {
                new ProductModel
                {
                    Id = 1,
                    Name = "Doombringer (Champion Sword)",
                    Price = 5999.99m
                },
                new ProductModel
                {
                    Id = 2,
                    Name = "Flamebellow (Balrog Blade)",
                    Price = 7459.55m
                },
                new ProductModel
                {
                    Id = 3,
                    Name = "The Grandfather (Colossus Blade)",
                    Price = 9999.98m
                }
            };

            var products = new List<Product>();
            foreach (var item in expected)
            {
                products.Add(new Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price
                });
            }

            var databaseMock = new Mock<IDatabaseService>();

            databaseMock
                .Setup(x => x.Products)
                .ReturnsDbSet(products);

            var query = new GetProductsListQuery(databaseMock.Object);

            // Act
            var actual = query.ExecuteAsync().GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int index = 0; index < expected.Count; index++)
            {
                Assert.Equal(expected[index].Id, actual[index].Id);
                Assert.Equal(expected[index].Name, actual[index].Name);
            }
        }

        [Fact(DisplayName = "Query Products on an Empty Database")]
        public void EmptyDatabase()
        {
            // Arrange
            var expected = new List<ProductModel>();

            var products = new List<Product>();
            foreach (var item in expected)
            {
                products.Add(new Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price
                });
            }

            var databaseMock = new Mock<IDatabaseService>();
            databaseMock
               .Setup(x => x.Products)
               .ReturnsDbSet(products);

            var query = new GetProductsListQuery(databaseMock.Object);

            // Act
            var actual = query.ExecuteAsync().GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
        }
    }
}

