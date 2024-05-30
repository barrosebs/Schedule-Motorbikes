using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    public class MotorcycleModel : ModelBase
    {
        [Column(TypeName = "char(4)")]
        public required string Year { get; set; }
        [Column(TypeName = "char(7)")]
        public required string LicensePlate { get; set; }
        [MaxLength(60)]
        public required string Model { get; set; }
        public DateTime? DateDelivery { get; set; }
        public Guid? DeliveryPersonId { get; set; }
        public bool IsDelivered { get; set; } = false;
        public override string ToString()
        {
            return $"Placa: {LicensePlate} - Modelo {Model} - Ano: {Year}";
        }
    }
}
