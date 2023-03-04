using System;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistance.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.Price)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.HasData(
                new Product { Id = 1, Name = "Doombringer (Champion Sword)", Price= 5999.99m },
                new Product { Id = 2, Name = "Flamebellow (Balrog Blade)", Price = 7459.55m },
                new Product { Id = 3, Name = "The Grandfather (Colossus Blade)", Price = 9999.98m });
        }
    }
}

