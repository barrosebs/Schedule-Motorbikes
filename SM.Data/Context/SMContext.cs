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

        public DbSet<DeliveryPersonModel> DeliveryPeople { get; set; }
        public DbSet<MotorcycleModel> Motorcycles { get; set; }
        public DbSet<AllocationModel> Allocations { get; set; }
        public DbSet<PlanModel> Plans { get; set; }
        public override int SaveChanges()
        {
            ValidateDeletions();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ValidateDeletions();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void ValidateDeletions()
        {
            var deletedDeliveryPeople = ChangeTracker.Entries<DeliveryPersonModel>()
                .Where(e => e.State == EntityState.Deleted)
                .Select(e => e.Entity.Id)
                .ToList();

            var deletedMotorcycles = ChangeTracker.Entries<MotorcycleModel>()
                .Where(e => e.State == EntityState.Deleted)
                .Select(e => e.Entity.Id)
                .ToList();

            if (deletedDeliveryPeople.Any() && Allocations.Any(a => deletedDeliveryPeople.Contains(a.DeliveryPersonID)))
            {
                throw new InvalidOperationException("Não é possível deletar entregadores que possuem alocações.");
            }

            if (deletedMotorcycles.Any() && Allocations.Any(a => deletedMotorcycles.Contains(a.MotorcycleID)))
            {
                throw new InvalidOperationException("Não é possível deletar motocicletas que possuem alocações.");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MotorcycleModel>()
            .HasIndex(p => p.LicensePlate)
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
