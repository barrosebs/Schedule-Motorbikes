using Microsoft.AspNetCore.Http;
using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Model
{
    public class DeliveryPersonVM
    {
        public Guid Id { get; set; } = new Guid();
        [Display(Name ="Nome Completo")]
        public required string FullName { get; set; }
        [Display(Name ="Email")]
        public required string Email{ get; set; }
        [Display(Name ="Número CNPJ")]
        public required string NumberCNPJ { get; set; }
        [Display(Name ="Número CNH")]
        public required string NumberCNH { get; set; }
        [Display(Name ="Categoria")]
        public ETypeCNH TypeCNH { get; set; }
        public DateTime DateCreated { get; set; }
        [Display(Name ="Validade CNH")]
        public required DateTime DateValidationCNH { get; set; }
        public bool isValidDeliveryPerson { get; set; } = false;
        [Display(Name = "Adicionar imagem CNH (Formato: .png ou .bmp)")]
        public required IFormFile ImageCNH { get; set; }
    }
}
