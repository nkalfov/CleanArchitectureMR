using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleanArchitecture.Presentation.Tests;

public class EmployeesControllerActionIndexTests
{
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

        var controller = new EmployeesController(getEmployeesListQueryMock.Object);

        // Act
        var actionResult = controller
            .Index()
            .GetAwaiter()
            .GetResult();

        var objectResult = actionResult as ViewResult;


#pragma warning disable CS8602 // Dereference of a possibly null reference.

        var actual = objectResult.Model as IList<EmployeeModel>;

#pragma warning restore CS8602 // Dereference of a possibly null reference.

        // Assert
        Assert.NotNull(objectResult);

        Assert.NotNull(actual);
        Assert.NotEmpty(actual);

#pragma warning disable CS8602 // Dereference of a possibly null reference.

        Assert.Equal(expected.Count, actual.Count);

#pragma warning restore CS8602 // Dereference of a possibly null reference.

        for (int index = 0; index < expected.Count; index++)
        {
            Assert.Equal(expected[index].Id, actual[index].Id);
            Assert.Equal(expected[index].Name, actual[index].Name);
        }
    }
}
