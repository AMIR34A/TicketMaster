using TicketMaster.Core.BuildingBlocks.Entities;
using TicketMaster.Core.Domain.Exceptions;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.Domain.Tickets;

public class Ticket : AggregateRoot<int>
{
    public int Row { get; private set; }

    public int SeatNumber { get; private set; }

    public TicketStatus Status { get; private set; }

    public Guid? BookedOrReservedBy { get; private set; } // Just a mock property for showing the user who booked or reserved the ticket

    public DateTime? BookedOrReservedAtUtc { get; private set; }

    public int EventId { get; private set; }

    private Ticket() { }

    public static Ticket Create(int eventId,
        int row,
        int seatNumber) => new()
        {
            EventId = eventId,
            Row = row,
            SeatNumber = seatNumber,
            Status = TicketStatus.Available
        };

    public void Reserve(Guid reservedBy)
    {
        if (Status != TicketStatus.Available)
            throw new DomainException(Error.Failure(description: "Ticket with row: {0} and seat number: {1} was already {2}.",
                parameters: [Row.ToString(), SeatNumber.ToString(), Status.ToString()]));

        Status = TicketStatus.Reserved;
        BookedOrReservedBy = reservedBy;
        BookedOrReservedAtUtc = DateTime.UtcNow;
    }

    public void Book(Guid bookedBy)
    {
        if (Status == TicketStatus.Booked)
            throw new DomainException(Error.Failure(description: "Ticket with row: {0} and seat number: {1} was already booked.",
                parameters: [Row.ToString(), SeatNumber.ToString()]));

        Status = TicketStatus.Booked;
        BookedOrReservedBy = bookedBy;
        BookedOrReservedAtUtc = DateTime.UtcNow;
    }
}