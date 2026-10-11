using TicketMaster.Core.Domain.Tickets;

namespace TicketMaster.Core.Contracts.Data.Repositories;

public interface ITicketRepository : IBaseRepository<Ticket, int>, IUnitOfWork
{
    Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(int eventId, CancellationToken cancellationToken = default);

    ValueTask<int> GetTicketsCountByEventIdAsync(int eventId, CancellationToken cancellationToken = default);

    ValueTask<int> GetReservedTicketsCountAsync(int eventId, CancellationToken cancellationToken = default);
}