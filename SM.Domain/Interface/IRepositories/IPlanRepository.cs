using Microsoft.AspNetCore.DataProtection;
using SM.Domain.Enum;
using SM.Domain.Interface.IRepository;
using SM.Domain.Model;

namespace SM.Domain.Interface.IRepositories
{
    public interface IPlanRepository : IRepositoryBase<PlanModel> { 
    Task<PlanModel> GetPlanByPlanAsync(EAllocationPlan ePlan);

    }
}
