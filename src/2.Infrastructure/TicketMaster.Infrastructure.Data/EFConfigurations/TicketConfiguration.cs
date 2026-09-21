using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketMaster.Core.Domain.Events;
using TicketMaster.Core.Domain.Tickets;

namespace TicketMaster.Infrastructure.Data.EFConfigurations;

internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Row)
               .IsRequired();

        builder.Property(p => p.SeatNumber)
               .IsRequired();

        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(10);

        builder.Property(p => p.BookedOrReservedBy)
               .IsRequired(false);

        builder.Property(p => p.BookedOrReservedAtUtc)
               .IsRequired(false);

        builder.HasOne<Event>()
               .WithMany()
               .HasForeignKey(p => p.EventId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.RowVersion)
               .IsRowVersion();
    }
}