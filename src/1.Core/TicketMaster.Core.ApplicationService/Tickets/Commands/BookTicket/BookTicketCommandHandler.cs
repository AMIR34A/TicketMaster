using MediatR;
using TicketMaster.Core.ApplicationService.Services;
using TicketMaster.Core.RequestResponse.Tickets.Commands.BookTicket;

namespace TicketMaster.Core.ApplicationService.Tickets.Commands.BookTicket;

public class BookTicketCommandHandler(TicketOperationsService ticketService) : IRequestHandler<BookTicketCommand>
{
    public async Task Handle(BookTicketCommand request, CancellationToken cancellationToken) =>
        await ticketService.BookAsync(request.TicketId, request.BookedBy, cancellationToken);
}