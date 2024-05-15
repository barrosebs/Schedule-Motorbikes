using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Model
{
    public class MotorcycleVM
    {
        public Guid? Id { get; set; }
        [Display(Name = "ANO")]
        public required string Year { get; set; }
        [Display(Name = "PLACA")]
        public required string LicensePlate { get; set; }
        [Display(Name = "MODELO")]
        public required string Model { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
