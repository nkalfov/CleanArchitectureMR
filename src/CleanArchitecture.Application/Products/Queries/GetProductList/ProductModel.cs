namespace CleanArchitecture.Application.Products.Queries.GetProductList
{
	public class ProductModel
	{
		public long Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal Price { get; set; }
	}
}
