using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts
{
    public interface IDatabaseService
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Sale> Sales { get; set; }

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}