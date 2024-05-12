using SM.Domain.Model;

namespace SM.Domain.Interface.IRepository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : ModelBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<object> CreateAsync(TEntity objeto);
        Task CreateRangeAsync(List<TEntity> ListaOfObjeto);
        Task UpdateAsync(TEntity objeto);
        Task RemoveAsync(TEntity objeto);
        Task RemoveByIdAsync(int id);
    }
}
