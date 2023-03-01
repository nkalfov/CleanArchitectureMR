using System.Collections.Generic;
using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Domain.Employees;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace CleanArchitecture.Application.Test.Employees.Queries
{
    public class GetEmployeesListQueryTests
    {
        [Fact(DisplayName = "Query All Three Employees")]
        public void QueryThreeEmployees()
        {
            // Arrange
            var expected = new List<EmployeeModel>()
            {
                new EmployeeModel
                {
                    Id = 1,
                    Name = "Anders Hejlsberg"
                },
                new EmployeeModel
                {
                    Id = 2,
                    Name = "Mads Torgensen"
                },
                new EmployeeModel
                {
                    Id = 3,
                    Name = "James Gosling"
                }
            };

            var employees = new List<Employee>();
            foreach (var item in expected)
            {
                employees.Add(new Employee
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            var databaseMock = new Mock<IDatabaseService>();

            databaseMock
                .Setup(x => x.Employees)
                .ReturnsDbSet(employees);

            var query = new GetEmployeesListQuery(databaseMock.Object);

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

        [Fact(DisplayName = "Query Employees on an Empty Database")]
        public void EmptyDataset()
        {
            // Arrange
            var expected = new List<EmployeeModel>();

            var employees = new List<Employee>();
            foreach (var item in expected)
            {
                employees.Add(new Employee
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            var databaseMock = new Mock<IDatabaseService>();

            databaseMock
                .Setup(x => x.Employees)
                .ReturnsDbSet(employees);

            var query = new GetEmployeesListQuery(databaseMock.Object);

            // Act
            var actual = query.ExecuteAsync().GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
        }
    }
}