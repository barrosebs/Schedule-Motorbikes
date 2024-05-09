using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    public class DeliveryPerson : BasicModel
    {
        [MaxLength(120)]
        public required string FullName { get; set; }
        [Column(TypeName = "char(14)")]
        public required string NumberCNPJ { get; set; }
        public required string NumberCNH { get; set; }
        public ETypeCNH TypeCNH { get; set; }
        public string UrlImageCNH { get; set; }
    }
}
