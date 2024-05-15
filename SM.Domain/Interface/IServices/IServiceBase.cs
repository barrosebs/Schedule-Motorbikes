using SM.Domain.Model;

namespace SM.Domain.Interface.IService
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : ModelBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<object> CreateAsync(TEntity objeto);
        Task CreateRangeAsync(List<TEntity> ListOfObject);
        Task UpdateAsync(TEntity objeto);
        Task RemoveAsync(TEntity objeto);
        Task RemoveByIdAsync(Guid id);
    }
}
