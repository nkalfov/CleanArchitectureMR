namespace CleanArchitecture.Application.Customers.Queries
{
    public interface IGetCustomersListQuery
    {
        Task<IList<CustomerModel>> ExecuteAsync();
    }
}