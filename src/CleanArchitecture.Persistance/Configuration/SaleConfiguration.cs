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
                .HasPrecision(5, 2);

            builder.HasOne(x => x.Customer);

            builder
                .Navigation(x => x.Customer)
                .IsRequired()
                .AutoInclude();

            builder.HasOne(x => x.Employee);

            builder
                .Navigation(x => x.Employee)
                .IsRequired()
                .AutoInclude();

            builder.HasOne(x => x.Product);

            builder
                .Navigation(x => x.Product)
                .IsRequired()
                .AutoInclude();
        }
    }
}

