namespace CleanArchitecture.Domain
{
    public class Employee : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}