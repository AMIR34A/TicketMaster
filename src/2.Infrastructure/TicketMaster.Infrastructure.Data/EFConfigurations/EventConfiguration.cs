using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketMaster.Core.Domain.Events;

namespace TicketMaster.Infrastructure.Data.EFConfigurations;

internal class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(200);

        builder.Property(p => p.Type)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(15);

        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(10);

        builder.Property(p => p.DateTimeUtc)
               .IsRequired();

        builder.OwnsOne(p => p.DurationMinute, durationBuilder =>
        {
            durationBuilder.Property(p => p.Value)
                           .IsRequired()
                           .HasColumnName("DurationMinute");
        });

        builder.OwnsOne(p => p.Capacity, capacityBuilder =>
        {
            capacityBuilder.Property(p => p.Value)
                           .IsRequired()
                           .HasColumnName("Capacity");
        });
    }
}