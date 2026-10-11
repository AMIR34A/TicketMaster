using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketMaster.Core.BuildingBlocks.Entities;

namespace TicketMaster.Infrastructure.Data;

public class TicketMasterDbContext : DbContext
{
    private readonly IMediator _mediator;

    public TicketMasterDbContext(DbContextOptions<TicketMasterDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var aggregatesWithEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(a => a.Events.Any())
            .ToList();

        var domainEvents = aggregatesWithEvents
            .SelectMany(a => a.Events)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent, cancellationToken);

        aggregatesWithEvents.ForEach(a => a.ClearEvents());

        return result;
    }
}