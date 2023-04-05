using System;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistance.Configuration
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.TotalPrice)
                .IsRequired()
                .HasPrecision(10, 2);

            builder
                .Ignore(x => x.TotalPrice);

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Sales)
                .HasForeignKey(x => x.CustomerId)
                .HasPrincipalKey(x => x.Id);

            builder
                .HasOne(x => x.Employee)
                .WithMany(x => x.Sales)
                .HasForeignKey(x => x.EmployeeId)
                .HasPrincipalKey(x => x.Id);

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.Sales)
                .HasForeignKey(x => x.ProductId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}

