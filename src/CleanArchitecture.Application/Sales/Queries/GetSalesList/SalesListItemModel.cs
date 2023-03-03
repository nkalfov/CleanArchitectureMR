using System;

namespace CleanArchitecture.Application.Sales.Queries.GetSalesList
{
	public class SalesListItemModel
	{
		public long Id { get; set; }
		public DateTimeOffset Date { get; set; }
		public string CustomerName { get; set; } = string.Empty;
		public string EmployeeName { get; set; } = string.Empty;
		public string ProductName { get; set; } = string.Empty;
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
	}
}	