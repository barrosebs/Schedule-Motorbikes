using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.ViewModel
{
    public class PlanVM
    {
        public Guid Id { get; set; }
        [Display(Name ="Limite de dias")]
        public int LimitDayPlan { get; set; }
        [Display(Name ="Tipo")]
        public EAllocationPlan EPlan { get; set; }
        [Display(Name ="Valor")]
        public decimal Value { get; set; }
    }
}
