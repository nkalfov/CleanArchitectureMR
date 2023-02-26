using Xunit;
using CleanArchitecture.Domain.Employees;

namespace CleanArchitecture.Domain.Test.Employees
{
    public class EmployeeTest
    {
        [Theory(DisplayName = "Initializing Employees")]
        [InlineData(1, "Emerald Warden")]
        [InlineData(2, "Flint Beastwood")]
        [InlineData(3, "Armadon")]
        public void InitializingEmployees(long id, string name)
        {
            var actual = new Employee
            {
                Id = id,
                Name = name
            };

            Assert.Equal(id, actual.Id);
            Assert.Equal(name, actual.Name);
        }
    }
}