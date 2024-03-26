using HD.Domain.Tickets.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HD.Infra.Mappings;

public class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasColumnType("varchar(100)");
        builder.Property(p => p.Description).IsRequired().HasColumnType("varchar(500)");
        builder.Property(p => p.Solution).HasColumnType("varchar(500)");
        builder.Property(p => p.Priority);

        builder.OwnsMany(p => p.Comments, c =>
        {
            c.ToTable("Comments");

            c.Property(p => p.Description).HasColumnName("Description").HasColumnType("varchar(500)");
            c.Property(p => p.CreatedIn).HasColumnName("CreatedIn");
            c.Property(p => p.EmployeeId).HasColumnName("EmployeeId");
        });
    }
}
