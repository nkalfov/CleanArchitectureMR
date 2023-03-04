using System;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistance.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new Customer { Id = 1, Name = "John Doe" },
                new Customer { Id = 2, Name = "Dennis Ritchie" },
                new Customer { Id = 3, Name = "Bjarne Stroustrup" });
        }
    }
}

