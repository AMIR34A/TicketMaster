using TicketMaster.Core.BuildingBlocks.Events;

namespace TicketMaster.Core.BuildingBlocks.Entities;

public interface IAggregateRoot
{
    void ClearEvents();

    IReadOnlyCollection<IDomainEvent> Events { get; }
}