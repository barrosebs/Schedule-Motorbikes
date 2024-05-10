
using SM.Domain.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Models
{
    public class BikeModel :BasicModel
    {
        [Column(TypeName = "char(4)")]
        [ReadOnly(true)]
        public required string Age { get; set; }
        [ReadOnly(true)]
        public required string Model { get; set; }
        public required string LicenseTag{ get; set; }
    }
}
