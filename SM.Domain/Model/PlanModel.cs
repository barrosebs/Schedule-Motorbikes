using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    [Table("plan")]
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
