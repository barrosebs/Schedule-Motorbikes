﻿using SM.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    public class DeliveryPersonModel : ModelBase
    {
        [MaxLength(120)]
        public required string FullName { get; set; }
        [Column(TypeName = "char(18)")]
        public required string NumberCNPJ { get; set; }
        [Column(TypeName = "char(11)")]
        public required string NumberCNH { get; set; }
        public ETypeCNH TypeCNH { get; set; }
        [MaxLength(150)]
        public string? UrlImageCNH { get; set; }
    }
}
