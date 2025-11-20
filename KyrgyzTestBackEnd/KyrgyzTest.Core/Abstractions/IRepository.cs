namespace KyrgyzTest.Core.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<List<TEntity>> GetAllAsync();
    
    Task<TEntity?> GetByIdAsync(long id);
    
    void Add(TEntity entity);
    
    void Remove(TEntity entity);
    
    void Update(TEntity entity);
}