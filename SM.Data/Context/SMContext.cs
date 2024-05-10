using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SM.Domain.Model;
using SM.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace SM.Data.Context
{
    public class SMContext : IdentityDbContext<UserModel, IdentityRole<int>, int>
    {
        public SMContext(DbContextOptions<SMContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<BikeModel> Bikes { get; set; }
        public DbSet<DeliveryPersonModel> DeliveryPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BikeModel>()
            .HasIndex(p => p.LicenseTag)
            .IsUnique();

            modelBuilder.Entity<DeliveryPersonModel>()
            .HasIndex(p => p.NumberCNPJ)
            .IsUnique();

            modelBuilder.Entity<DeliveryPersonModel>()
            .HasIndex(p => p.NumberCNH)
            .IsUnique();
        }
    }
}
