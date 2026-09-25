namespace TicketMaster.Core.RequestResponse.Tickets.Commands.CreateTickets;

public sealed record CreateTicketsCommandResponse(int ReservedTicketsCount,
    int TotalTicketsCount,
    int Capacity,
    IEnumerable<TicketInfo> ReservedTicketsInfo);