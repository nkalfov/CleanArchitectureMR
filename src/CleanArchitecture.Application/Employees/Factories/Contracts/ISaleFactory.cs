using System;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Employees.Factories.Contracts
{
    public interface ISaleFactory
    {
        Sale Create(
            DateTimeOffset date,
            Customer customer,
            Employee employee,
            Product product,
            int quantity);
    }
}

