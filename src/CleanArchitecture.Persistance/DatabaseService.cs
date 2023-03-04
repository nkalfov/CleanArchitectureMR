﻿using System;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Domain;
using CleanArchitecture.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Persistance
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;

            Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get;  set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("Application");

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
            new EmployeeConfiguration().Configure(modelBuilder.Entity<Employee>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new SaleConfiguration().Configure(modelBuilder.Entity<Sale>());
        }
    }
}

