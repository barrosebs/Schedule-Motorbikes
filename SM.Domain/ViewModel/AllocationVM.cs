
using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Model
{
    public class AllocationVM
    {
        public Guid? Id { get; set; }
        [Display(Name = "Entregador")]
        public DeliveryPersonModel DeliveryPerson { get; set; }
        [Display(Name = "Moto")]
        public MotorcycleModel Motorcycle { get; set; }
        [Display(Name = "Plano")]
        public EAllocationPlan Plan { get; set; }
        [Display(Name = "Inicio da Alocação")]
        public DateTime StartDateToAllocation { get; set; }
        [Display(Name = "Entrega Prevista")]
        public DateTime EndDateToAllocation { get; set; }
        [Display(Name = "Entrega")]
        public DateTime DeliveryDate { get; set; }
    }
}
