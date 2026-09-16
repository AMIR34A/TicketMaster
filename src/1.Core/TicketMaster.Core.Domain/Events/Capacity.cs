using TicketMaster.Core.BuildingBlocks.ValueObjects;
using TicketMaster.Core.Domain.Exceptions;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.Domain.Events;

public class Capacity : ValueObject<Capacity>
{
    public int Value { get; init; }

    public Capacity(int value)
    {
        if (value < 0)
            throw new DomainException(Error.Validation(parameters: [nameof(Capacity)]));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}