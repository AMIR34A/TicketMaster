using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketMaster.Core.BuildingBlocks.Entities;
using TicketMaster.Core.Contracts.Data.Repositories;

namespace TicketMaster.Infrastructure.Data.Repositories;

public class BaseRepository<TEntity, TDbContext, TId> : IBaseRepository<TEntity, TId>, IUnitOfWork
    where TEntity : AggregateRoot<TId>
    where TDbContext : DbContext
    where TId : struct
{
    protected readonly TDbContext _dbContext;

    public BaseRepository(TDbContext dbContext) => _dbContext = dbContext;

    #region UnitOfWork Implementation
    public void BeginTransaction() => _dbContext.Database.BeginTransaction();

    public void CommitTransaction() => _dbContext.Database.CommitTransaction();

    public async Task CommitTransactionAsync() => await _dbContext.Database.CommitTransactionAsync();

    public int Commit() => _dbContext.SaveChanges();

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);

    public void RollbackTransaction() => _dbContext.Database.RollbackTransaction();

    public async Task RollbackTransactionAsync() => await _dbContext.Database.RollbackTransactionAsync();
    #endregion

    #region Repository Implementation
    public TEntity? Get(TId id) => _dbContext.Set<TEntity>().Find(id);

    public async Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default) => await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);

    public void Insert(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default) => await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

    public void Delete(TId id)
    {
        TEntity? entity = _dbContext.Set<TEntity>().Find(id);

        if (entity is not null && !entity.Id.Equals(default))
            _dbContext.Set<TEntity>().Remove(entity);
    }

    public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

    public bool Exists(Expression<Func<TEntity, bool>> expression) => _dbContext.Set<TEntity>().Any(expression);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) => await _dbContext.Set<TEntity>().AnyAsync(expression, cancellationToken);
    #endregion
}