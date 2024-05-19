
using SM.Domain.Enum;

namespace SM.Domain.Model
{
    public class AllocationModel : ModelBase
    {
        public DeliveryPersonModel DeliveryPerson { get; set; }
        public MotorcycleModel Motorcycle { get; set; }
        public EAllocationPlan Plan { get; set; }
        public DateTime StartDateToAllocation { get; set; }
        public DateTime EndDateToAllocation { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
