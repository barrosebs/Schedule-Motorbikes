using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.Domain.Model;
using SM.Domain.Models;

namespace SM.Data.Context
{
    public class SMContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public SMContext(DbContextOptions<SMContext> options)
            : base(options)
        {
        }

        public DbSet<Bike> Bikes { get; set; }
        public DbSet<DeliveryPerson> DeliveryPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bike>()
            .HasIndex(p => p.LicenseTag)
            .IsUnique();

            modelBuilder.Entity<DeliveryPerson>()
            .HasIndex(p => p.NumberCNPJ)
            .IsUnique();

            modelBuilder.Entity<DeliveryPerson>()
            .HasIndex(p => p.NumberCNH)
            .IsUnique();
        }
    }
}
