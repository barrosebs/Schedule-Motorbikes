using SM.Domain.Enum;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Interface.IServices;
using SM.Domain.Model;
using SM.Service.Shared;

namespace SM.Service
{
    internal class PlanService : ServiceBase<PlanModel>, IPlanService
    {
        private readonly IPlanRepository _repository;

        public PlanService(IPlanRepository Repository) : base(Repository)
        {
            _repository = Repository;
        }

        public Task<PlanModel> GetPlanByPlanAsync(EAllocationPlan ePlan)
        {
            return _repository.GetPlanByPlanAsync(ePlan);
        }
    }
}
