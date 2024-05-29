using SM.Domain.Enum;
using SM.Domain.Interface.IRepository;
using SM.Domain.Model;

namespace SM.Domain.Interface.IServices
{
    public interface IPlanService : IRepositoryBase<PlanModel> {
    Task<PlanModel> GetPlanByPlanAsync(EAllocationPlan ePlan);

    }
}
