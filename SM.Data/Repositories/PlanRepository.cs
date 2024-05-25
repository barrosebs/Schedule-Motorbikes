using SM.Data.Context;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class PlanRepository : RepositoryBase<PlanModel>, IPlanRepository
    {
        public PlanRepository(SMContext context) : base(context) { }
    }
}
