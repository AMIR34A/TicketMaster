using TicketMaster.Core.Domain.Tickets;

namespace TicketMaster.Core.Contracts.Data.Repositories;

public interface ITicketRepository : IBaseRepository<Ticket, int>, IUnitOfWork
{
    ValueTask<int> GetTicketsCountByEventIdAsync(int eventId, CancellationToken cancellationToken = default);
}