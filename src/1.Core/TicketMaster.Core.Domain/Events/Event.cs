using TicketMaster.Core.BuildingBlocks.Entities;

namespace TicketMaster.Core.Domain.Events;

public class Event : AggregateRoot<int>
{
    public string Title { get; private set; } = default!;

    public EventType Type { get; private set; }

    public EventStatus Status { get; private set; }

    public DateTime DateTimeUtc { get; private set; }

    public Duration DurationMinute { get; private set; } = default!;

    public Capacity Capacity { get; private set; } = default!;

    public Guid OwnerId { get; private set; } // Just a mock property for event owner

    private Event() { }

    public Event Create(string title,
        EventType type,
        EventStatus status,
        Duration durationMinute,
        Capacity capacity,
        Guid ownerId) => new()
        {
            Title = title,
            Type = type,
            Status = status,
            DateTimeUtc = DateTime.UtcNow,
            DurationMinute = durationMinute,
            Capacity = capacity,
            OwnerId = ownerId
        };

    public bool IsAvailable() => Status == EventStatus.OnSale;
}