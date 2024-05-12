using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    public class DeliveryPersonVM
    {
        public string? Id { get; set; }
        [Display(Name ="Nome Completo")]
        public required string FullName { get; set; }
        [Display(Name ="Número CNPJ")]
        public required string NumberCNPJ { get; set; }
        [Display(Name ="Número CNH")]
        public required string NumberCNH { get; set; }
        [Display(Name ="Categoria")]
        public ETypeCNH TypeCNH { get; set; }
        [Display(Name = "Adicionar imagem CNH (Formto: png ou bmp)")]
        public string? UrlImageCNH { get; set; }
        public DateTime DateCreated { get; set; }
        
    }
}
