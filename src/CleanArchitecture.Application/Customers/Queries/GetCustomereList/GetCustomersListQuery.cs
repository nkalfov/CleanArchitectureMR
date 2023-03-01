using System.Linq.Expressions;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomereList
{
    public class GetCustomersListQuery : IGetCustomersListQuery
    {
        private readonly IDatabaseService _database;

        public GetCustomersListQuery(IDatabaseService database)
        {
            _database = database;
        }

        private static readonly Expression<Func<Customer, CustomerModel>> _projection =
            x => new CustomerModel
            {
                Id = x.Id,
                Name = x.Name
            };

        public async Task<IList<CustomerModel>> ExecuteAsync()
        {
            var customers = _database
                .Customers
                .Select(_projection);

            return await customers.ToListAsync();
        }
    }
}