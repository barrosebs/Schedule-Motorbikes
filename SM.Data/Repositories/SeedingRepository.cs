using SM.Data.Context;
using SM.Domain.Enum;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class SeedingRepository
    {
        private readonly SMContext _Context;

        public SeedingRepository(SMContext context)
        {
            _Context = context;
        }


        public void Seed()
        {
            if (_Context.Plans.Any() || _Context.Plans.Any())
            {
                return;//Verica se o banco foi populado
            }
            PlanModel FirstPlan = new PlanModel
            {
                EPlan = EAllocationPlan.Basic,
                LimitDayPlan = 7,
                Value = 30,
                DateCreated = DateTime.Now,
            };
            PlanModel SecundPlan = new PlanModel
            {
                EPlan = EAllocationPlan.Standard,
                LimitDayPlan = 15,
                Value = 28,
                DateCreated = DateTime.Now,
            };
            PlanModel ThirdPlan = new PlanModel
            {
                EPlan = EAllocationPlan.Plus,
                LimitDayPlan = 30,
                Value = 22,
                DateCreated = DateTime.Now,
            };
            PlanModel FourthPlan = new PlanModel
            {
                EPlan = EAllocationPlan.Enterprise,
                LimitDayPlan = 45,
                Value = 20,
                DateCreated = DateTime.Now,
            };
            PlanModel FifthPlan = new PlanModel
            {
                EPlan = EAllocationPlan.Premium,
                LimitDayPlan = 50,
                Value = 18,
                DateCreated = DateTime.Now,
            };
            _Context.Plans.AddRangeAsync(FirstPlan,SecundPlan, ThirdPlan, FourthPlan, FifthPlan);
            _Context.SaveChanges();
        }
    }
}
