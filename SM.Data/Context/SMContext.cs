using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.Domain.Models;

namespace SM.Data.Context
{
    public class SMContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public SMContext(DbContextOptions<SMContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }
    }
}
