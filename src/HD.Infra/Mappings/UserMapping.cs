using HD.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HD.Infra.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName).IsRequired().HasColumnType("varchar(30)");
        builder.Property(p => p.LastName).IsRequired().HasColumnType("varchar(30)");
        builder.Property(p => p.Registration).IsRequired().HasColumnType("varchar(8)");
    }
}
