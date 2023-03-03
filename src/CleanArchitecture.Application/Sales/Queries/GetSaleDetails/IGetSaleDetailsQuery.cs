namespace CleanArchitecture.Application.Sales.Queries.GetSaleDetails
{
	public interface IGetSaleDetailsQuery
	{
		Task<SaleDetailsModel?> ExecuteAsync(long id);
	}
}

