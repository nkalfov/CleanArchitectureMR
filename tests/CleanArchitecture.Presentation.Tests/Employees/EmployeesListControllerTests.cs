using System;
using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Presentation.Controllers.Employees;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleanArchitecture.Presentation.Tests.Employees
{
    public class EmployeesListControllerTests
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

            var controller = new EmployeesListController(
                getEmployeesListQueryMock.Object);

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

            Assert.IsType<ViewResult>(actionResult);

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);

            Assert.Equal(expected.Count, actual.Count);

            for (int index = 0; index < expected.Count; index++)
            {
                Assert.Equal(expected[index].Id, actual[index].Id);
                Assert.Equal(expected[index].Name, actual[index].Name);
            }
        }
    }
}

