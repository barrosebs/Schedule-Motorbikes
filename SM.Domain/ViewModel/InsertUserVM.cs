using System.ComponentModel.DataAnnotations;

namespace SM.Domain.ViewModel
{
    public class InsertUserVM
    {
        public Guid Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string FullName { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O tamanho mínimo do campo {0} é de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Password { get; set; }

        [Display(Name = "Confirmação de Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MaxLength(16, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O tamanho mínimo do campo {0} é de {1} caracteres.")]
        [Compare(nameof(Password), ErrorMessage = "A confirmação da senha não confere com a senha.")]
        public string ConfirmPassword { get; set; }
    }
}
