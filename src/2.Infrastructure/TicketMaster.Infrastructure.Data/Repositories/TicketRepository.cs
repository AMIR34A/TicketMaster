using Microsoft.EntityFrameworkCore;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.Domain.Tickets;

namespace TicketMaster.Infrastructure.Data.Repositories;

internal class TicketRepository : BaseRepository<Ticket, TicketMasterDbContext, int>, ITicketRepository
{
    public TicketRepository(TicketMasterDbContext dbContext) : base(dbContext)
    {
    }

    public async ValueTask<int> GetTicketsCountByEventIdAsync(int eventId,
        CancellationToken cancellationToken = default) => await _dbContext.Set<Ticket>()
        .Where(t => t.EventId == eventId)
        .CountAsync(cancellationToken);
}