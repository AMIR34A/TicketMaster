using MediatR;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.RequestResponse.Tickets.Queries.GetTickets;

namespace TicketMaster.Core.ApplicationService.Tickets.Queries.GetTickets;

public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, GetTicketsQueryResponse>
{
    private readonly ITicketRepository _ticketRepository;

    public GetTicketsQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<GetTicketsQueryResponse> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = (await _ticketRepository.GetTicketsByEventIdAsync(request.EventId, cancellationToken))
             .Select(t => new TicketInfo(
                 t.Row,
                 t.SeatNumber,
                 t.Status,
                 t.BookedOrReservedBy,
                 t.BookedOrReservedAtUtc)
             );

        return new(tickets);
    }
}