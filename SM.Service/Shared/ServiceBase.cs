using SM.Domain.Interface.IRepository;
using SM.Domain.Interface.IService;
using SM.Domain.Model;
using System;

namespace SM.Service.Shared
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : ModelBase
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase) =>
            _repositoryBase = repositoryBase;

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _repositoryBase.GetAllAsync();
        public virtual async Task<TEntity?> GetByIdAsync(Guid id) => await _repositoryBase.GetByIdAsync(id);
        public virtual async Task<object> CreateAsync(TEntity objeto) => await _repositoryBase.CreateAsync(objeto);
        public virtual async Task CreateRangeAsync(List<TEntity> ListOfObject) => await _repositoryBase.CreateRangeAsync(ListOfObject);
        public virtual async Task UpdateAsync(TEntity objeto) => await _repositoryBase.UpdateAsync(objeto);
        public virtual async Task RemoveAsync(TEntity objeto) => await _repositoryBase.RemoveAsync(objeto);
        public virtual async Task RemoveByIdAsync(Guid id) => await _repositoryBase.RemoveByIdAsync(id);
        public void Dispose() => _repositoryBase.Dispose();
    }
}
