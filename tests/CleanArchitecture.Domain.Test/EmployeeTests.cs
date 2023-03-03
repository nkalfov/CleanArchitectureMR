using Xunit;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Domain.Test
{
    public class EmployeeTests
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