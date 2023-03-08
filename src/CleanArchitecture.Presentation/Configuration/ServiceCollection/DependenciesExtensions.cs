using System;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Customers.Queries.GetCustomereList;
using CleanArchitecture.Application.Employees.Queries.GetEmployeesList;
using CleanArchitecture.Application.Products.Queries.GetProductList;
using CleanArchitecture.Application.Sales.Queries.GetSaleDetails;
using CleanArchitecture.Application.Sales.Queries.GetSalesList;
using CleanArchitecture.Common.Options;
using CleanArchitecture.Common.Services;
using CleanArchitecture.Common.Services.Contracts;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Persistance;

namespace CleanArchitecture.Presentation.Configuration.ServiceCollection
{
    public static class DependenciesExtensions
    {
        public static void AddIOptions(this WebApplicationBuilder builder)
        {
            var inventoryOptions = builder
                .Configuration
                .GetSection(InventoryOptions.SettingKey);

            builder
                .Services
                .Configure<InventoryOptions>(inventoryOptions);
        }

        public static void AddCommon(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddScoped<IDateService, DateService>();
        }

        public static void AddPersistance(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddScoped<IDatabaseService, DatabaseService>();
        }


        public static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddHttpClient<IInventoryService, InventoryService>();
        }

        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddScoped<IGetCustomersListQuery, GetCustomersListQuery>();

            builder
                .Services
                .AddScoped<IGetEmployeesListQuery, GetEmployeesListQuery>();

            builder
                .Services
                .AddScoped<IGetProductsListQuery, GetProductsListQuery>();

            builder
                .Services
                .AddScoped<IGetSalesListQuery, GetSalesListQuery>();

            builder
                .Services
                .AddScoped<IGetSaleDetailsQuery, GetSaleDetailsQuery>();
        }
    }
}
