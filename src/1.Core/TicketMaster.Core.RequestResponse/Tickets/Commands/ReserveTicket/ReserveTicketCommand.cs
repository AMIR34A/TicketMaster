using MediatR;

namespace TicketMaster.Core.RequestResponse.Tickets.Commands.ReserveTicket;

public sealed record ReserveTicketCommand(int TicketId, Guid ReservedBy) : IRequest;