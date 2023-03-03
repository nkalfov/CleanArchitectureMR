using System.Collections.Generic;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using CleanArchitecture.Application.Customers.Queries.GetCustomereList;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Test.Customers.Queries
{
    public class GetCustomersListQueryTests
    {
        [Fact(DisplayName = "Query All Three Customers")]
        public void QueryThreeCustomers()
        {
            // Arrange
            var expected = new List<CustomerModel>
            {
                new CustomerModel
                {
                    Id = 1,
                    Name = "John Doe"
                },
                new CustomerModel
                {
                    Id = 1,
                    Name = "Dennis Ritchie"
                },
                new CustomerModel
                {
                    Id = 1,
                    Name = "Bjarne Stroustrup"
                }
            };

            var customers = new List<Customer>();
            foreach (var item in expected)
            {
                customers.Add(new Customer
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            var databaseMock = new Mock<IDatabaseService>();

            databaseMock
                .Setup(x => x.Customers)
                .ReturnsDbSet(customers);

            var query = new GetCustomersListQuery(databaseMock.Object);

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

        [Fact(DisplayName = "Query Customers on an Empty Database")]
        public void EmptyDatabase()
        {
            // Arrange
            var expected = new List<CustomerModel>();

            var customers = new List<Customer>();
            foreach (var item in expected)
            {
                customers.Add(new Customer
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            var databaseMock = new Mock<IDatabaseService>();
            databaseMock
               .Setup(x => x.Customers)
               .ReturnsDbSet(customers);

            var query = new GetCustomersListQuery(databaseMock.Object);

            // Act
            var actual = query.ExecuteAsync().GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
        }
    }
}