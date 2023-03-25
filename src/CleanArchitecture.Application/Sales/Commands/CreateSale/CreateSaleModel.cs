using System;
namespace CleanArchitecture.Application.Sales.Commands.CreateSale
{
    public class CreateSaleModel
    {
        public long CustomerId { get; set; }
        public long EmployeeId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

