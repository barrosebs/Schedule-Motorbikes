using SM.Domain.Enum;

namespace SM.Domain.Model
{
    public class PlanModel : ModelBase
    {
        public int LimitDayPlan { get; set; }
        public EAllocationPlan EPlan { get; set; }
        public decimal Value { get; set; }
        public override string ToString()
        {
            return $"Plano {EPlan}: {LimitDayPlan} dias com custo de R$ {Value.ToString("F2")} por dia.";
        }
    }
}
