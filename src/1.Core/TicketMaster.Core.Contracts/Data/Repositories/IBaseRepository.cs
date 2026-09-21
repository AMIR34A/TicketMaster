using System.Linq.Expressions;
using TicketMaster.Core.BuildingBlocks.Entities;

namespace TicketMaster.Core.Contracts.Data.Repositories;

public interface IBaseRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : struct
{
    TEntity? Get(TId id);

    Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default);

    void Insert(TEntity entity);

    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Delete(TId id);

    void Delete(TEntity entity);

    bool Exists(Expression<Func<TEntity, bool>> expression);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
}