
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.ViewModel
{
    public class BikeVM
    {

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public required string Age { get; set; }
        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public required string Model { get; set; }
        [Display(Name = "Placa")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public required string LicenseTag { get; set; }
    }
}
