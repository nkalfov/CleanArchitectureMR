using Xunit;
using CleanArchitecture.Domain.Customers;

namespace CleanArchitecture.Domain.Test.Customers
{
    public class CustomerTest
    {
        [Theory(DisplayName = "Initializing Customers")]
        [InlineData(1, "John Doe")]
        [InlineData(2, "Matthew Renze")]
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