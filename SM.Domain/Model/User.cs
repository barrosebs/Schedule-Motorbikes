using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(120)]
        public string FullName { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Column(TypeName = "char(11)")]
        public string CPF { get; set; }
        public string? Uri { get; set; }
        public Guid EmpresaId { get; set; }
        [NotMapped]
        public int Age
        {
            get => (int)Math.Floor((DateTime.Now - DateOfBirth).TotalDays / 365.25);
        }
    }
}