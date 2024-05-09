﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Model
{
    [Owned]

    public class BasicModel
    {
        [Key]
        [ReadOnly(true)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public Guid? UserId { get; set; }
    }
}