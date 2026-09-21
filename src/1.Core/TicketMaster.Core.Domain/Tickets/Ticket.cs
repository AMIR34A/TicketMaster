using TicketMaster.Core.BuildingBlocks.Entities;
using TicketMaster.Core.Domain.Exceptions;
using TicketMaster.Shared.Utilities.Exceptions;
using TicketMaster.Shared.Utilities.Guards;
using TicketMaster.Shared.Utilities.Guards.GuardClauses;

namespace TicketMaster.Core.Domain.Tickets;

public class Ticket : AggregateRoot<int>
{
    public Row Row { get; private set; } = default!;

    public SeatNumber SeatNumber { get; private set; } = default!;

    public TicketStatus Status { get; private set; }

    public Guid? BookedOrReservedBy { get; private set; } // Just a mock property for showing the user who booked or reserved the ticket

    public DateTime? BookedOrReservedAtUtc { get; private set; }

    public int EventId { get; private set; }

    public byte[] RowVersion { get; private set; } = default!;

    private Ticket() { }

    public static Ticket Create(int eventId,
        Row row,
        SeatNumber seatNumber)
    {
        Guard.ThrowExceptionIf.Null(row, new DomainException(Error.Validation(parameters: [nameof(row)])));
        Guard.ThrowExceptionIf.Null(seatNumber, new DomainException(Error.Validation(parameters: [nameof(seatNumber)])));

        return new()
        {
            EventId = eventId,
            Row = row,
            SeatNumber = seatNumber,
            Status = TicketStatus.Available
        };
    }
    public void Reserve(Guid reservedBy)
    {
        if (Status != TicketStatus.Available)
            throw new DomainException(Error.Failure(description: "Ticket with row: {0} and seat number: {1} was already {2}.",
                parameters: [Row.Value.ToString(), SeatNumber.Value.ToString(), Status.ToString()]));

        Status = TicketStatus.Reserved;
        BookedOrReservedBy = reservedBy;
        BookedOrReservedAtUtc = DateTime.UtcNow;
    }

    public void Book(Guid bookedBy)
    {
        if (Status != TicketStatus.Reserved)
            throw new DomainException(Error.Failure(description: "Ticket must be reserved before booking."));

        if (BookedOrReservedBy != bookedBy)
            throw new DomainException(Error.Failure(description: "Ticket is reserved by a different user."));

        if (Status == TicketStatus.Booked)
            throw new DomainException(Error.Failure(description: "Ticket with row: {0} and seat number: {1} was already booked.",
                parameters: [Row.Value.ToString(), SeatNumber.Value.ToString()]));

        Status = TicketStatus.Booked;
        BookedOrReservedBy = bookedBy;
        BookedOrReservedAtUtc = DateTime.UtcNow;
    }
}