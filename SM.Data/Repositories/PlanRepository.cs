using SM.Data.Context;
using SM.Domain.Enum;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class PlanRepository : RepositoryBase<PlanModel>, IPlanRepository
    {
        public PlanRepository(SMContext context) : base(context) { }

        public async Task<PlanModel> GetPlanByPlanAsync(EAllocationPlan ePlan)
        {
            var result = _context.Plans.FirstOrDefault(p => p.EPlan == ePlan);
            return result;
        }
    }
}
