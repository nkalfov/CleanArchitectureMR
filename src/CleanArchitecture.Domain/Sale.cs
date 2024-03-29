namespace CleanArchitecture.Domain
{
    public class Sale : IEntity<long>
    {
        public long Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity {get;set;}
        public DateTimeOffset Date { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        public long CustomerId { get; set; }
        public Customer Customer { get; set; } = new Customer();

        public long EmployeeId { get; set; }
        public Employee Employee { get; set; } = new Employee();

        public long ProductId { get; set; }
        public Product Product { get; set; } = new Product();
    }
}