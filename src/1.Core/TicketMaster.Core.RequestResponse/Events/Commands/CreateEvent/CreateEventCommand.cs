using MediatR;
using TicketMaster.Core.Domain.Events;

namespace TicketMaster.Core.RequestResponse.Events.Commands.CreateEvent;

public sealed record CreateEventCommand(string Title,
    EventType Type,
    DateTime DateTimeUtc,
    Duration DurationMinute,
    Capacity Capacity,
    Guid OwnerId
) : IRequest<CreateEventCommandResponse>;