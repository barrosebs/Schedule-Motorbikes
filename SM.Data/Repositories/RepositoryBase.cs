using Microsoft.EntityFrameworkCore;
using SM.Data.Context;
using SM.Domain.Interface.IRepository;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : ModelBase
    {
        protected readonly SMContext _context;

        public RepositoryBase(SMContext context) => _context = context;
        
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>()
                                                                                        .AsNoTracking()
                                                                                        .ToListAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id) => await _context.Set<TEntity>().FindAsync(id);

        public virtual async Task<object> CreateAsync(TEntity objeto)
        {
            try
            {
                _context.Add(objeto);
                await _context.SaveChangesAsync();
                return objeto.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task CreateRangeAsync(List<TEntity> ListaOfObjeto)
        {
            try
            {
                await _context.AddRangeAsync(ListaOfObjeto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task UpdateAsync(TEntity objeto)
        {
            try
            {
                _context.Entry(objeto).State = EntityState.Modified;
                _context.Update(objeto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task RemoveAsync(TEntity objeto)
        {
            try
            {
                _context.Set<TEntity>().Remove(objeto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task RemoveByIdAsync(int id)
        {
            try
            {
                var objeto = await GetByIdAsync(id);
                if (objeto == null)
                    throw new Exception("O registro não existe na base de dados.");
                await RemoveAsync(objeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose() =>
        _context.Dispose();
    }
}
