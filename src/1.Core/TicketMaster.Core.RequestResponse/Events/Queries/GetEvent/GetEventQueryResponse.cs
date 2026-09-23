using TicketMaster.Core.Domain.Events;

namespace TicketMaster.Core.RequestResponse.Events.Queries.GetEvent;

public sealed record GetEventQueryResponse(string Title,
    EventType Type,
    EventStatus Status,
    DateTime DateTimeUtc,
    int DurationMinute,
    int Capacity,
    Guid OwnerId
);