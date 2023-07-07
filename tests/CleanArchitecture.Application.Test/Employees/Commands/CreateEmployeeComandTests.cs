using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Employees.Factories.Contracts;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace CleanArchitecture.Application.Tests.Employees.Commands
{
    public class CreateEmployeeComandTests
    {
        [Fact(DisplayName = "ExecuteAsync Saves an Employee in the Database")]
        public void CreateAnEmployee()
        {
            // Arrange
            var createEmployeeModel = new BaseEmployeeModel
            {
                Name = "John Doe"
            };

            var employee = new Employee
            {
                Name = createEmployeeModel.Name
            };

            var employeeFactoryMock = new Mock<IEmployeeFactory>();

            employeeFactoryMock
                .Setup(x => x.Create(
                    It.IsAny<BaseEmployeeModel>()))
                .Returns(employee);

            var employeeList = new List<Employee>();

            var databaseServiceMock = new Mock<IDatabaseService>();

            databaseServiceMock
                .Setup(x => x.Employees)
                .ReturnsDbSet(employeeList);

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

            databaseServiceMock
                .Setup(x => x.Employees.AddAsync(
                    It.IsAny<Employee>(),
                    default))
                .Callback(
                    (Employee entity, CancellationToken token) =>
                    {
                        entity.Id = employeeList.Count + 1;
                        employeeList.Add(entity);
                    })
                .Returns(
                    (Employee entity, CancellationToken token) =>
                    {
                        return ValueTask.FromResult(default(EntityEntry<Employee>));
                    });

#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            databaseServiceMock
                .Setup(x => x.SaveAsync(default))
                .Returns(Task.CompletedTask);

            var inventoryServiceMock = new Mock<IInventoryService>();
            inventoryServiceMock
                .Setup(x => x.NotifySaleOccurredAsync(
                    It.IsAny<long>(),
                    It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            var createEmployeeCommand = new CreateEmployeeCommand(
                employeeFactoryMock.Object,
                databaseServiceMock.Object);

            // Act
            createEmployeeCommand.ExecuteAsync(createEmployeeModel).GetAwaiter().GetResult();

            // Assert
            databaseServiceMock.Verify(
                x => x.Employees.AddAsync(
                    It.IsAny<Employee>(),
                    default),
                Times.Once);

            databaseServiceMock.Verify(
                x => x.SaveAsync(default),
                Times.Once);
        }
    }
}

