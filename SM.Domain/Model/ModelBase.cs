using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Domain.Model
{
    [Owned]

    public class ModelBase
    {
        [Key]
        [ReadOnly(true)]
        [Column(name:"id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column(name:"datecreated")]
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }

        public ModelBase()
        {
            DateCreated = DateTime.Now;
        }
    }
}
