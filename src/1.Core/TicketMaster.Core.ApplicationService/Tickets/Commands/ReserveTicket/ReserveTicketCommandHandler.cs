using MediatR;
using TicketMaster.Core.ApplicationService.Services;
using TicketMaster.Core.RequestResponse.Tickets.Commands.ReserveTicket;

namespace TicketMaster.Core.ApplicationService.Tickets.Commands.ReserveTicket;

public class ReserveTicketCommandHandler(TicketOperationsService ticketService) : IRequestHandler<ReserveTicketCommand>
{
    public async Task Handle(ReserveTicketCommand request, CancellationToken cancellationToken) =>
        await ticketService.ReserveAsync(request.TicketId, request.ReservedBy, cancellationToken);
}