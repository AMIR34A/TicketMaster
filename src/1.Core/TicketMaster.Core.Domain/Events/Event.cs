using TicketMaster.Core.BuildingBlocks.Entities;
using TicketMaster.Core.Domain.Exceptions;
using TicketMaster.Shared.Utilities.Exceptions;
using TicketMaster.Shared.Utilities.Guards;
using TicketMaster.Shared.Utilities.Guards.GuardClauses;

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

    public static Event Create(string title,
        EventType type,
        EventStatus status,
        Duration durationMinute,
        Capacity capacity,
        Guid ownerId)
    {
        Guard.ThrowExceptionIf.Empty(title, new DomainException(Error.Validation(parameters: [nameof(title)])));
        Guard.ThrowExceptionIf.Null(durationMinute, new DomainException(Error.Validation(parameters: [nameof(durationMinute)])));
        Guard.ThrowExceptionIf.Null(capacity, new DomainException(Error.Validation(parameters: [nameof(capacity)])));

        return new()
        {
            Title = title,
            Type = type,
            Status = status,
            DateTimeUtc = DateTime.UtcNow,
            DurationMinute = durationMinute,
            Capacity = capacity,
            OwnerId = ownerId
        };
    }
    public bool IsAvailable() => Status == EventStatus.OnSale;
}