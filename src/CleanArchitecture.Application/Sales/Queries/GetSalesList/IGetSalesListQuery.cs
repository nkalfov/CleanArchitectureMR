using System;
namespace CleanArchitecture.Application.Sales.Queries.GetSalesList
{
	public interface IGetSalesListQuery
	{
		Task<IList<SalesListItemModel>> ExecuteAsync();
	}
}

