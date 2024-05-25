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

        public override string ToString()
        {
            return $"Placa: {LicensePlate} - Modelo {Model} - Ano: {Year}";
        }
    }
}
