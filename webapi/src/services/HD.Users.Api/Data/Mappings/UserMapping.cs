using HD.Core.DomainObjects;
using HD.Users.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HD.Users.Api.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName).IsRequired().HasColumnType("varchar(100)");
        builder.Property(u => u.LastName).IsRequired().HasColumnType("varchar(100)");
        builder.OwnsOne(u => u.Email, ue =>
        {
            ue.Property(e => e.Address).IsRequired().HasColumnName("Email").HasColumnType($"varchar({Email.AddressMaxLength})");
        });
        builder.OwnsOne(u => u.Department, ud =>
        {
            ud.Property(d => d.Code);
            ud.Property(d => d.Name);
        });

        builder.ToTable("Users");
    }
}
