using HD.Domain.Departments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HD.Infra.Mappings;

public class DepartmentMapping : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code).IsRequired().HasColumnType("varchar(6)");
        builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(100)");

        builder.HasMany(p => p.Users).WithOne(p => p.Department).HasForeignKey(p => p.DepartmentId);
    }
}
