using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.Domain.Events;

namespace TicketMaster.Infrastructure.Data.Repositories;

public class EventRepository : BaseRepository<Event, TicketMasterDbContext, int>, IEventRepository
{
    public EventRepository(TicketMasterDbContext dbContext) : base(dbContext)
    {
    }
}