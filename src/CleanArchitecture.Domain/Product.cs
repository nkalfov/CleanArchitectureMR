namespace CleanArchitecture.Domain
{
    public class Product : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}