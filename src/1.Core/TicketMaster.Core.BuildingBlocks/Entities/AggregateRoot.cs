using System.Reflection;
using TicketMaster.Core.BuildingBlocks.Events;

namespace TicketMaster.Core.BuildingBlocks.Entities;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : struct
{
    private readonly List<IDomainEvent> _events;

    public IReadOnlyCollection<IDomainEvent> Events => _events;

    protected AggregateRoot() => _events = [];

    public AggregateRoot(IEnumerable<IDomainEvent> events) : this()
    {
        if (events is null || !events.Any())
            return;

        foreach (var @event in events)
            Mutate(@event);
    }

    protected void Apply(IDomainEvent @event)
    {
        Mutate(@event);
        AddEvent(@event);
    }

    public void ClearEvents() => _events.Clear();

    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);

    private void Mutate(IDomainEvent @event)
    {
        var onMethod = this.GetType().GetMethod("On", BindingFlags.Instance | BindingFlags.NonPublic, [@event.GetType()]);
        onMethod?.Invoke(this, new[] { @event });
    }
}