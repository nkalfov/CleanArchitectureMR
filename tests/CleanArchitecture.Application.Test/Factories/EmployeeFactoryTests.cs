using CleanArchitecture.Application.Employees.Factories;
using CleanArchitecture.Application.Employees.ViewModels;
using Xunit;

namespace CleanArchitecture.Application.Tests.Factories
{
    public class EmployeeFactoryTests
    {
        [Fact(DisplayName = "Create an Employee from a CreateEmployeeModel")]
        public void CreatingAnEmployeeFromACreateEmployeeModel()
        {
            // Arrange
            var expectedId = 0L;
            var expectedName = "John Doe";
            var createEmployeeModel = new BaseEmployeeModel
            {
                Name = expectedName
            };

            var employeeFactory = new EmployeeFactory();

            // Act
            var actual = employeeFactory.Create(createEmployeeModel);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedName, actual.Name);
        }
    }
}

