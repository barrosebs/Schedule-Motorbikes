using System.ComponentModel.DataAnnotations;

namespace SM.Domain.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public required string User { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public required string Password { get; set; }
        [Required]
        [Display(Name = "Lembrar de mim")]
        public bool Remember { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
