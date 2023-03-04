using Xunit;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Domain.Test
{
    public class CustomerTests
    {
        [Theory(DisplayName = "Initializing Customers")]
        [InlineData(1, "John Doe")]
        [InlineData(2, "Dennis Ritchie")]
        [InlineData(3, "Bjarne Stroustrup")]
        public void InitializingCustomers(long id, string name)
        {
            var actual = new Customer
            {
                Id = id,
                Name = name
            };

            Assert.Equal(id, actual.Id);
            Assert.Equal(name, actual.Name);
        }
    }
}