using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Employees.Commands.EditEmployee;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CleanArchitecture.Application.Tests.Employees.Commands
{
    public class EditEmployeeCommandTests
    {
        [Fact(DisplayName = "Editing an existing employee changes its name")]
        public void EditAnEmployee()
        {
            // Arrange
            var employeeModel = new EmployeeModel
            {
                Id = 6,
                Name = "Valkyrie"
            };

            var employee = new Employee
            {
                Id = employeeModel.Id,
                Name = "Valkirie"
            };

            var employeesDbSet = new Mock<DbSet<Employee>>();

            employeesDbSet
                .Setup(x => x.FindAsync(It.IsAny<long>()))
                .Returns(ValueTask.FromResult<Employee?>(employee));

            var databaseServiceMock = new Mock<IDatabaseService>();

            databaseServiceMock
                .Setup(x => x.Employees)
                .Returns(employeesDbSet.Object);

            databaseServiceMock
                .Setup(x => x.SaveAsync(default))
                .Returns(Task.CompletedTask);

            var editEmployeeCommand = new EditEmployeeCommand(databaseServiceMock.Object);

            // Act
            editEmployeeCommand
                .ExecuteAsync(employeeModel)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.Equal(employeeModel.Id, employee.Id);
            Assert.Equal(employeeModel.Name, employee.Name);

            employeesDbSet.Verify(
                x => x.FindAsync(It.IsAny<long>()),
                Times.Once);

            databaseServiceMock.Verify(
                x => x.Employees,
                Times.Once);

            databaseServiceMock.Verify(
                x => x.SaveAsync(default),
                Times.Once);
        }

        [Fact(DisplayName = ("Editing an unexisting employee does nothing"))]
        public void EditUnexistingEmployee()
        {
            // Arrange
            var employeeModel = new EmployeeModel
            {
                Id = 154,
                Name = "Zephyr"
            };

            var employeesDbSet = new Mock<DbSet<Employee>>();

            employeesDbSet
                .Setup(x => x.FindAsync(It.IsAny<long>()))
                .Returns(ValueTask.FromResult<Employee?>(default));

            var databaseServiceMock = new Mock<IDatabaseService>();

            databaseServiceMock
                .Setup(x => x.Employees)
                .Returns(employeesDbSet.Object);

            databaseServiceMock
                .Setup(x => x.SaveAsync(default))

                .Returns(Task.CompletedTask);

            var editEmployeeCommand = new EditEmployeeCommand(databaseServiceMock.Object);

            // Act
            editEmployeeCommand
                .ExecuteAsync(employeeModel)
                .GetAwaiter()
                .GetResult();

            // Assert
            employeesDbSet.Verify(
                x => x.FindAsync(It.IsAny<long>()),
                Times.Once);

            databaseServiceMock.Verify(
                x => x.Employees,
                Times.Once);

            databaseServiceMock.Verify(
                x => x.SaveAsync(default),
                Times.Never);
        }
    }
}

