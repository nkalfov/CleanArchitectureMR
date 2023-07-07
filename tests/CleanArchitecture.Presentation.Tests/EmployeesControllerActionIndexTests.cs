using CleanArchitecture.Application.Employees.Commands.CreateEmployee;
using CleanArchitecture.Application.Employees.Commands.EditEmployee;
using CleanArchitecture.Application.Employees.Queries.GetEmployeeById;
using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleanArchitecture.Presentation.Tests;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

public class EmployeesControllerActionIndexTests
{
    private readonly Mock<IGetEmployeesListQuery> _dummyEmployeesListQuery;
    private readonly Mock<IGetEmployeeByIdQuery> _dummyEmployeeByIdQuery;
    private readonly Mock<ICreateEmployeeCommand> _dummyCreateEmployeeCommand;
    private readonly Mock<IEditEmployeeCommand> _dummyEditEmployeeCommand;

    public EmployeesControllerActionIndexTests()
    {
        _dummyEmployeesListQuery = new Mock<IGetEmployeesListQuery>();
        _dummyEmployeeByIdQuery = new Mock<IGetEmployeeByIdQuery>();
        _dummyCreateEmployeeCommand = new Mock<ICreateEmployeeCommand>();
        _dummyEditEmployeeCommand = new Mock<IEditEmployeeCommand>();
    }

    [Fact(DisplayName = "Get All Three Employees")]
    public void GettingThreeEmployees()
    {
        IList<EmployeeModel> expected = new List<EmployeeModel>
        {
            new EmployeeModel
            {
                Id = 1,
                Name = "Emerald Warden"
            },
            new EmployeeModel
            {
                Id = 2,
                Name = "Flint Beastwood"
            },
            new EmployeeModel
            {
                Id = 3,
                Name = "Armadon"
            }
        };

        // Arrange
        var getEmployeesListQueryMock = new Mock<IGetEmployeesListQuery>();

        getEmployeesListQueryMock
            .Setup(x => x.ExecuteAsync())
            .Returns(Task.FromResult(expected));

        var controller = new EmployeesController(
            getEmployeesListQueryMock.Object,
            _dummyEmployeeByIdQuery.Object,
            _dummyCreateEmployeeCommand.Object,
            _dummyEditEmployeeCommand.Object);

        // Act
        var actionResult = controller
            .Index()
            .GetAwaiter()
            .GetResult();

        var viewResult = actionResult as ViewResult;

        var actual = viewResult.Model as IList<EmployeeModel>;

        // Assert
        getEmployeesListQueryMock.Verify(
            x => x.ExecuteAsync(),
            Times.Once());

        _dummyEmployeeByIdQuery.Verify(
            x => x.ExecuteAsync(It.IsAny<long>()),
            Times.Never);

        _dummyCreateEmployeeCommand.Verify(
            x => x.ExecuteAsync(It.IsAny<BaseEmployeeModel>()),
            Times.Never);

        _dummyEditEmployeeCommand.Verify(
            x => x.ExecuteAsync(It.IsAny<EmployeeModel>()),
            Times.Never);


        Assert.NotNull(viewResult);
        Assert.IsType<ViewResult>(viewResult);

        Assert.NotNull(actual);
        Assert.NotEmpty(actual);

        Assert.Equal(expected.Count, actual.Count);

        for (int index = 0; index < expected.Count; index++)
        {
            Assert.Equal(expected[index].Id, actual[index].Id);
            Assert.Equal(expected[index].Name, actual[index].Name);
        }
    }

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

        var controller = new EmployeesController(
            _dummyEmployeesListQuery.Object,
            _dummyEmployeeByIdQuery.Object,
            createEmployeeMock.Object,
            _dummyEditEmployeeCommand.Object);

        // Act
        var actionResult = controller
            .Create(dummy)
            .GetAwaiter()
            .GetResult();

        var viewResult = actionResult as RedirectToActionResult;

        // Assert
        _dummyEmployeesListQuery.Verify(
            x => x.ExecuteAsync(),
            Times.Never);

        _dummyEmployeeByIdQuery.Verify(
            x => x.ExecuteAsync(It.IsAny<long>()),
            Times.Never);

        createEmployeeMock.Verify(
            x => x.ExecuteAsync(It.IsAny<BaseEmployeeModel>()),
            Times.Once);

        _dummyEditEmployeeCommand.Verify(
            x => x.ExecuteAsync(It.IsAny<EmployeeModel>()),
            Times.Never);

        Assert.IsType<RedirectToActionResult>(actionResult);
        Assert.Null(viewResult.ControllerName);
        Assert.Equal(nameof(controller.Index), viewResult.ActionName);
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

        var controller = new EmployeesController(
            _dummyEmployeesListQuery.Object,
            _dummyEmployeeByIdQuery.Object,
            createEmployeeMock.Object,
            _dummyEditEmployeeCommand.Object);

        controller.ModelState.AddModelError(nameof(dummy.Name), "Some Error Stating Something");

        // Act
        var actionResult = controller
            .Create(dummy)
            .GetAwaiter()
            .GetResult();

        var viewResult = actionResult as ViewResult;

        // Assert
        _dummyEmployeesListQuery.Verify(
            x => x.ExecuteAsync(),
            Times.Never);

        createEmployeeMock.Verify(
            x => x.ExecuteAsync(It.IsAny<BaseEmployeeModel>()),
            Times.Never);
    }
}

#pragma warning restore CS8602 // Dereference of a possibly null reference.
