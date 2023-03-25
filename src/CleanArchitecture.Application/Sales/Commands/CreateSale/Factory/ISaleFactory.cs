using System;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Sales.Commands.CreateSale.Factory
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

