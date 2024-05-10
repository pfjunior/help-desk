using HD.Tickets.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HD.Tickets.Data.Mappings;

public class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.TicketNumber).HasDefaultValueSql("NEXT VALUE FOR TicketNumberSequence");

        builder.Property(t => t.Subject).IsRequired().HasColumnType("varchar(100)");

        builder.Property(t => t.Description).IsRequired().HasColumnType("varchar(1000)");

        builder.Property(t => t.Solution).HasColumnType("varchar(1000)");

        builder.OwnsOne(t => t.User, u =>
        {
            u.Property(tu => tu.UserName).HasColumnName("UserName");
            u.Property(tu => tu.Email).HasColumnName("Email");
            u.Property(tu => tu.Department).HasColumnName("Department");
        });

        builder.HasMany(t => t.Comments).WithOne(c => c.Ticket).HasForeignKey(c => c.TicketId);
    }
}
