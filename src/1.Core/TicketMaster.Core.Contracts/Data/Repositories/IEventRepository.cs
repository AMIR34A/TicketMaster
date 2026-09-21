using TicketMaster.Core.Domain.Events;

namespace TicketMaster.Core.Contracts.Data.Repositories;

public interface IEventRepository : IBaseRepository<Event, int>, IUnitOfWork
{
}