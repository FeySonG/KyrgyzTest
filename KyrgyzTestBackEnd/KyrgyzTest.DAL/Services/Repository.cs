using KyrgyzTest.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.DAL.Services;

//Base Repository
public abstract class Repository<TEntity>(AppDbContext dbContext) : IRepository<TEntity> where TEntity : Entity
{

    public Task<List<TEntity>> GetAllAsync() => dbContext
        .Set<TEntity>()
        .ToListAsync();

    public Task<TEntity?> GetByIdAsync(long id) => dbContext
        .Set<TEntity>()
        .FirstOrDefaultAsync(e => e.Id == id);

    public void Add(TEntity entity) => dbContext
        .Set<TEntity>()
        .Add(entity);

    public void Remove(TEntity entity) => dbContext
        .Set<TEntity>()
        .Remove(entity);

    public void Update(TEntity entity) => dbContext
        .Set<TEntity>()
        .Update(entity);
}