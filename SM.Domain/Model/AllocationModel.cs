
using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    public class AllocationModel : ModelBase
    {
        public EAllocationPlan EPlan { get; set; }
        public DateTime StartDateToAllocation { get; set; }
        public DateTime EndDateToAllocation { get; set; }
        public DateTime DeliveryDate { get; set; }
        [ForeignKey(nameof(DeliveryPersonID))]
        public Guid DeliveryPersonID { get; set; }
        public required DeliveryPersonModel DeliveryPerson { get; set; }
        public required MotorcycleModel Motorcycle { get; set; }
        [ForeignKey(nameof(MotorcycleID))]
        public Guid MotorcycleID { get; set; }
        public bool IsAllocation { get; set; } = false;
        public decimal AmountToPay { get; set; }
    }
}
