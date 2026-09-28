using MediatR;

namespace TicketMaster.Core.RequestResponse.Tickets.Commands.BookTicket;

public sealed record BookTicketCommand(int TicketId, Guid BookedBy) : IRequest;
