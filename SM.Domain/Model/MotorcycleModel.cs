using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    [Table("motorcycle")]
    public class MotorcycleModel : ModelBase
    {
        [Column(name: "year",TypeName = "char(4)")]
        public required string Year { get; set; }
        [Column(name: "license_plate", TypeName = "char(7)")]
        public required string LicensePlate { get; set; }
        [MaxLength(60)]
        [Column(name: "model")]
        public required string Model { get; set; }
        public DateTime? DateDelivery { get; set; }
        public Guid? DeliveryPersonId { get; set; }
        [Column(name: "isdelivered")]
        public bool IsDelivered { get; set; } = false;
        public override string ToString()
        {
            return $"Placa: {LicensePlate} - Modelo {Model} - Ano: {Year}";
        }
    }
}
