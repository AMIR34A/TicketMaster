using MediatR;
using TicketMaster.Core.Domain.Tickets;

namespace TicketMaster.Core.RequestResponse.Tickets.Commands.CreateTickets;

public sealed record TicketInfo(Row Row, SeatNumber SeatNumber);

public sealed record CreateTicketsCommand(int EventId, IReadOnlyCollection<TicketInfo> TicketsInfo) : IRequest<CreateTicketsCommandResponse>;