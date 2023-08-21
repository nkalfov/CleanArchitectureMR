using CleanArchitecture.Application.Employees.Commands.EditEmployee;
using CleanArchitecture.Application.Employees.Queries.GetEmployeeById;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Presentation.Controllers.Employees;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace CleanArchitecture.Presentation.Tests.Employees
{
    public class EmployeesEditControllerTests
    {
        [Fact(DisplayName = "Get Edit Employee With an Existing ID Returns an Employee")]
        public void GetEditEmployeeWithExistingId()
        {
            // Arrange
            var employee = new EmployeeModel
            {
                Id = 7,
                Name = "Valkyrie"
            };

            var getEmployeeByIdQuery = new Mock<IGetEmployeeByIdQuery>();
            var editEmployeeCommand = new Mock<IEditEmployeeCommand>();

            getEmployeeByIdQuery
                .Setup(x => x.ExecuteAsync(It.IsAny<long>()))
                .Returns(Task.FromResult<EmployeeModel?>(employee));

            var controller = new EmployeesEditController(
                getEmployeeByIdQuery.Object,
                editEmployeeCommand.Object);

            // Act
            var actionResult = controller
                .Edit(employee.Id)
                .GetAwaiter()
                .GetResult();

            var viewResult = actionResult as ViewResult;

            var actual = viewResult.Model as EmployeeModel;

            // Assert
            getEmployeeByIdQuery.Verify(
                x => x.ExecuteAsync(It.IsAny<long>()),
                Times.Once);

            editEmployeeCommand.Verify(
                x => x.ExecuteAsync(It.IsAny<EmployeeModel>()),
                Times.Never);

            Assert.IsType<ViewResult>(actionResult);
            Assert.NotNull(actual);
            Assert.Equal(employee.Id, actual.Id);
            Assert.Equal(employee.Name, actual.Name);
        }

        [Fact(DisplayName = "Get Edit Employee With an Nonexisting ID Returns Not Found")]
        public void GetEditEmployeeWithNonexistingId()
        {
            // Arrange
            var getEmployeeByIdQuery = new Mock<IGetEmployeeByIdQuery>();
            getEmployeeByIdQuery
                .Setup(x => x.ExecuteAsync(It.IsAny<long>()))
                .Returns(Task.FromResult<EmployeeModel?>(default));

            var editEmployeeCommand = new Mock<IEditEmployeeCommand>();

            var controller = new EmployeesEditController(
                getEmployeeByIdQuery.Object,
                editEmployeeCommand.Object);

            // Act
            var actionResult = controller
                .Edit(-1)
                .GetAwaiter()
                .GetResult();

            // Assert
            getEmployeeByIdQuery.Verify(
                x => x.ExecuteAsync(It.IsAny<long>()),
                Times.Once);

            editEmployeeCommand.Verify(
                x => x.ExecuteAsync(It.IsAny<EmployeeModel>()),
                Times.Never);

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact(DisplayName = "Post Edit Employee Redirect To Employee Index No Matter if Employee Exists or Not")]
        public void PostEditEmployee()
        {
            // Arrange
            var getEmployeeByIdQuery = new Mock<IGetEmployeeByIdQuery>();
            var editEmployeeCommand = new Mock<IEditEmployeeCommand>();

            editEmployeeCommand
                .Setup(x => x.ExecuteAsync(
                    It.IsAny<EmployeeModel>()))
                .Returns(Task.CompletedTask);

            var controller = new EmployeesEditController(
                getEmployeeByIdQuery.Object,
                editEmployeeCommand.Object);

            // Act
            var actionResult = controller
                .Edit(new EmployeeModel())
                .GetAwaiter()
                .GetResult();

            // Assert
            getEmployeeByIdQuery.Verify(
                x => x.ExecuteAsync(It.IsAny<long>()),
                Times.Never);

            editEmployeeCommand.Verify(
                x => x.ExecuteAsync(It.IsAny<EmployeeModel>()),
                Times.Once);

            Assert.IsType<RedirectToActionResult>(actionResult);
        }
    }
}

#pragma warning restore CS8602 // Dereference of a possibly null reference.