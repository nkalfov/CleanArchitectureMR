using System;
using CleanArchitecture.Common.Dimensions;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistance.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(EmployeeDimensions.LengthMax);

            builder.HasData(
                new Employee { Id = 1, Name = "Emerald Warden" },
                new Employee { Id = 2, Name = "Flint Beastwood" },
                new Employee { Id = 3, Name = "Armadon" });
        }
    }
}

