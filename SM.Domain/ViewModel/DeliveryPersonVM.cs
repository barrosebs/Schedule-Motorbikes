using Microsoft.AspNetCore.Http;
using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    public class DeliveryPersonVM
    {
        public Guid? Id { get; set; }
        [Display(Name ="Nome Completo")]
        public required string FullName { get; set; }
        [Display(Name ="Número CNPJ")]
        public required string NumberCNPJ { get; set; }
        [Display(Name ="Número CNH")]
        public required string NumberCNH { get; set; }
        [Display(Name ="Categoria")]
        public ETypeCNH TypeCNH { get; set; }
        public DateTime DateCreated { get; set; }
        [Display(Name = "Adicionar imagem CNH (Formato: .png ou .bmp)")]
        public required IFormFile ImageCNH { get; set; }
    }
}
