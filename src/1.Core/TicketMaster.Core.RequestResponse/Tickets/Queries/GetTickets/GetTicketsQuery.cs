using MediatR;

namespace TicketMaster.Core.RequestResponse.Tickets.Queries.GetTickets;

public sealed record GetTicketsQuery(int EventId) : IRequest<GetTicketsQueryResponse>;