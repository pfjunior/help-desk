using HD.Domain.Employees.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HD.Infra.Mappings;

public class EmployeeMapping : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName).IsRequired().HasColumnType("varchar(30)");
        builder.Property(p => p.LastName).IsRequired().HasColumnType("varchar(30)");
        builder.Property(p => p.Registration).IsRequired().HasColumnType("varchar(8)");
    }
}
