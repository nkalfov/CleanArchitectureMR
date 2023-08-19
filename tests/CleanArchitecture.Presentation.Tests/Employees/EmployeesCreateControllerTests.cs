using CleanArchitecture.Application.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Presentation.Controllers.Employees;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleanArchitecture.Presentation.Tests.Employees
{
    public class EmployeesCreateControllerTests
    {
        [Fact(DisplayName = "Create Employee")]
        public void CreateEmployee()
        {
            // Arrange
            var dummy = new BaseEmployeeModel
            {
                Name = "Lorem ipsum"
            };

            var createEmployeeMock = new Mock<ICreateEmployeeCommand>();
            createEmployeeMock
                .Setup(x => x.ExecuteAsync(It.IsAny<BaseEmployeeModel>()))
                .Returns(Task.CompletedTask);

            var controller = new EmployeesCreateController(createEmployeeMock.Object);

            // Act
            var actionResult = controller
                .Create(dummy)
                .GetAwaiter()
                .GetResult();

            var viewResult = actionResult as RedirectToActionResult;

            // Assert
            createEmployeeMock.Verify(
                x => x.ExecuteAsync(It.IsAny<BaseEmployeeModel>()),
                Times.Once);

            Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Equal("EmployeesList", viewResult.ControllerName);
            Assert.Equal("Index", viewResult.ActionName);
        }

        [Fact(DisplayName = "Create Employee With Invalid ModelState Returns the Same Action")]
        public void CreateEmployeeWithLessThanMinimumLength()
        {
            // Arrange
            var dummy = new BaseEmployeeModel
            {
                Name = "Dummy"
            };

            var createEmployeeMock = new Mock<ICreateEmployeeCommand>();
            createEmployeeMock
                .Setup(x => x.ExecuteAsync(It.IsAny<BaseEmployeeModel>()))
                .Returns(Task.CompletedTask);

            var controller = new EmployeesCreateController(
                createEmployeeMock.Object);

            controller.ModelState.AddModelError(nameof(dummy.Name), "Some Error Stating Something");

            // Act
            var actionResult = controller
                .Create(dummy)
                .GetAwaiter()
                .GetResult();

            var viewResult = actionResult as ViewResult;

            // Assert
            createEmployeeMock.Verify(
                x => x.ExecuteAsync(It.IsAny<BaseEmployeeModel>()),
                Times.Never);
        }
    }
}

