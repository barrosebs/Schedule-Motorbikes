
using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using SM.Domain.Validation;

namespace SM.Domain.Model
{
    public class AllocationVM
    {
        public Guid? Id { get; set; }
        [Display(Name = "Entregador")]
        public DeliveryPersonModel? DeliveryPerson { get; set; }
        public MotorcycleModel? Motorcycle { get; set; }
        [Display(Name = "Moto")]
        public Guid MotorcycleID { get; set; }
        public EAllocationPlan EPlan { get; set; }
        public string? Plan { get; set; }
        [Display(Name = "Plano")]
        public Guid PlanID { get; set; }
        [Display(Name = "Inicio da Alocação")]
        [StartDateToAllocation]
        public DateTime StartDateToAllocation { get; set; } = DateTime.Now;
        [Display(Name = "Entrega Prevista")]
        public DateTime EndDateToAllocation { get; set; }
        [Display(Name = "Entrega")]
        public DateTime DeliveryDate { get; set; }
        public bool IsAllocation { get; set; } = false;
        public decimal ValueDay { get; set; }
        public decimal DailyRate { get; set; }
        public decimal Sum { get; set; }
        public double UsedDays { get; set; }

        public override string ToString()
        {
            if(this.DeliveryDate < this.EndDateToAllocation)
            {
                return $"Entrega para {DeliveryDate.ToString("dd/MM/yyyy")}, {this.UsedDays} dias de uso com um custo de R$ {ValueDay.ToString("F2")} mais multa de {DailyRate.ToString("P")} sobre os dias restantes.\n Total a PAGAR RS {Sum.ToString("F2")}.";
            }
            else if (this.DeliveryDate > this.EndDateToAllocation)
            {
                return $"{this.UsedDays} dia(s) contabilizados com  valor de R$ {ValueDay.ToString("F2")} mais multa de R$ 50,00. TOTAL A PAGAR RS {Sum.ToString("F2")}.";
            }
            else
            {
                return $"Parabéns {DeliveryDate.ToString("dd/MM/yyyy")} dentro do prazo de entrega. TOTAL A PAGAR RS {Sum}.";

            }
        }
    }
}
