using MediatR;
using TicketMaster.Core.BuildingBlocks.Events;

namespace TicketMaster.Core.Domain.Tickets;

public record TicketsSoldOut(int EventId, int ReservedCount) : IDomainEvent, IRequest;