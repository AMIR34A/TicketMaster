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
        DateTime dateTimeUtc,
        Duration durationMinute,
        Capacity capacity,
        Guid ownerId)
    {
        Guard.ThrowExceptionIf.Empty(title, new DomainException(Error.Validation(parameters: [nameof(title)])));
        Guard.ThrowExceptionIf.Null(durationMinute, new DomainException(Error.Validation(parameters: [nameof(durationMinute)])));
        Guard.ThrowExceptionIf.Null(capacity, new DomainException(Error.Validation(parameters: [nameof(capacity)])));
        Guard.ThrowExceptionIf.LessThanOrEqual(dateTimeUtc, DateTime.UtcNow, new DomainException(Error.Validation(parameters: [nameof(dateTimeUtc)])));

        return new()
        {
            Title = title,
            Type = type,
            Status = EventStatus.Draft,
            DateTimeUtc = dateTimeUtc,
            DurationMinute = durationMinute,
            Capacity = capacity,
            OwnerId = ownerId
        };
    }

    public bool IsAvailable() => Status == EventStatus.OnSale && DateTimeUtc > DateTime.UtcNow;

    public void Sell()
    {
        Guard.ThrowExceptionIf.NotEqual(Status, EventStatus.Draft,
            new DomainException(Error.Failure(description: "Event must be in draft status for stating selling.",
            parameters: [nameof(Sell)])));

        Guard.ThrowExceptionIf.LessThan(DateTimeUtc, DateTime.UtcNow,
            new DomainException(Error.Failure(description: "Event's time is over for setting to on sale.",
            parameters: [nameof(Sell)])));

        Status = EventStatus.OnSale;
    }

    public void SellOut(int reservedCount)
    {
        if (!IsAvailable())
            throw new DomainException(Error.Failure(description: "Event isn't available.",
                parameters: [nameof(SellOut)]));

        if (Capacity.Value > reservedCount)
            throw new DomainException(Error.Failure(description: "Event's had available tickets.",
                parameters: [nameof(SellOut)]));

        Status = EventStatus.SoldOut;
    }

    public void Cancel()
    {
        Guard.ThrowExceptionIf.Equal(Status, EventStatus.Finished,
            new DomainException(Error.Failure(description: "Event with finished status can't be cancelled.",
            parameters: [nameof(Cancel)])));

        Guard.ThrowExceptionIf.LessThan(DateTimeUtc, DateTime.UtcNow,
            new DomainException(Error.Failure(description: "Event's time is over for setting to cancelled.",
            parameters: [nameof(Cancel)])));

        Status = EventStatus.Cancelled;
    }

    public void Finish()
    {
        Guard.ThrowExceptionIf.Equal(Status, EventStatus.Finished,
            new DomainException(Error.Failure(description: "Event's status must be on sale on.",
            parameters: [nameof(Finish)])));

        Guard.ThrowExceptionIf.LessThan(DateTimeUtc, DateTime.UtcNow,
            new DomainException(Error.Failure(description: "Event's date time must be grater than current date time.",
            parameters: [nameof(Cancel)])));

        Status = EventStatus.Finished;
    }
}