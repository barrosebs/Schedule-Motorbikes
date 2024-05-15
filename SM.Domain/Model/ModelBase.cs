using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Model
{
    [Owned]

    public class ModelBase
    {
        [Key]
        [ReadOnly(true)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }

        public ModelBase()
        {
            DateCreated = DateTime.Now;
        }
    }
}
