namespace CleanArchitecture.Application.Customers.Queries.GetCustomereList
{
    public interface IGetCustomersListQuery
    {
        Task<IList<CustomerModel>> ExecuteAsync();
    }
}