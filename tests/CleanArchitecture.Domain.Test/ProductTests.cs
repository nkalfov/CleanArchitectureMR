using Xunit;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Domain.Test
{
    public class ProductTests
    {
        [Theory(DisplayName = "Initializing Products")]
        [InlineData(1, "Doombringer (Champion Sword)", 5999.99)]
        [InlineData(1, "Flamebellow (Balrog Blade)", 7459.55)]
        [InlineData(3, "The Grandfather (Colossus Blade)", 9999.98)]
        public void InitializingProducts(int id, string name, decimal price)
        {
            var actual = new Product
            {
                Id = id,
                Name = name,
                Price = price
            };

            Assert.Equal(id, actual.Id);
            Assert.Equal(name, actual.Name);
            Assert.Equal(price, actual.Price);
        }

    }
}