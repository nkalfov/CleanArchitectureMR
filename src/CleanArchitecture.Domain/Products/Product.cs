using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Products
{
    public class Product : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}