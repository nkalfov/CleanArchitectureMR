using CleanArchitecture.Application.Products.ViewModels;

namespace CleanArchitecture.Application.Products.Queries.GetProductList
{
	public interface IGetProductsListQuery
	{
		Task<IList<ProductModel>> ExecuteAsync();
	}
}

