
using SM.Data.Context;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class MotorcycleRepository : RepositoryBase<MotorcycleModel>, IMotorcycleRepository
    {
        public MotorcycleRepository(SMContext context) : base(context) { }
    }
}
