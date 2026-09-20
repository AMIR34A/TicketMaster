using TicketMaster.Core.BuildingBlocks.ValueObjects;
using TicketMaster.Core.Domain.Exceptions;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.Domain.Tickets;

public class Row : ValueObject<Row>
{
    public int Value { get; init; }

    public Row(int value)
    {
        if (value < 0)
            throw new DomainException(Error.Validation(parameters: [nameof(Row)]));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}