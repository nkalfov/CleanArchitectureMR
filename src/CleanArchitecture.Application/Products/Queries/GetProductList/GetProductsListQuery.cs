using System.Linq.Expressions;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Products.ViewModels;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Products.Queries.GetProductList
{
	public class GetProductsListQuery : IGetProductsListQuery
    {
        private readonly IDatabaseService _databaseService;

		public GetProductsListQuery(IDatabaseService databaseService)
		{
            _databaseService = databaseService;
		}

        private static readonly Expression<Func<Product, ProductModel>> _projection =
            x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            };

        public async Task<IList<ProductModel>> ExecuteAsync()
        {
            var products = _databaseService
                .Products
                .Select(_projection);

            return await products.ToListAsync();
        }
    }
}

