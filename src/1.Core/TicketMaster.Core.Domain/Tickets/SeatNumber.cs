using TicketMaster.Core.BuildingBlocks.ValueObjects;
using TicketMaster.Core.Domain.Exceptions;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.Domain.Tickets;

public class SeatNumber : ValueObject<SeatNumber>
{
    public int Value { get; init; }

    public SeatNumber(int value)
    {
        if (value < 0)
            throw new DomainException(Error.Validation(parameters: [nameof(SeatNumber)]));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}