using System.ComponentModel.DataAnnotations;

namespace SM.Domain.ViewModel
{
    public class ResetPasswordVM
    {
        [Required]
        public string Token { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Nova Senha")]
        [DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O tamanho mínimo do campo {0} é de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmação da Nova Senha")]
        [DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O tamanho mínimo do campo {0} é de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [Compare(nameof(NewPassword), ErrorMessage = "A confirmação da nova senha não confere com a nova senha.")]
        public string NewPasswordConfirmation { get; set; }
    }
}
