using TicketMaster.Core.Domain.Tickets;

namespace TicketMaster.Core.RequestResponse.Tickets.Queries.GetTickets;

public sealed record TicketInfo(
    Row Row,
    SeatNumber SeatNumber,
    TicketStatus Status,
    Guid? BookedOrReservedBy,
    DateTime? BookedOrReservedAtUtc);

public sealed record GetTicketsQueryResponse(IEnumerable<TicketInfo> TicketsInfo);
