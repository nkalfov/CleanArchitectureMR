namespace CleanArchitecture.Domain
{
    public class Customer : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}