
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Seed initial data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "admin@jilcabuyao", PasswordHash = "jesusislord", FirstName = "Admin", MiddleName = "JIL", LastName = "Cabuyao" },
                new User { Id = 2, UserName = "mia@jilcabuyao", PasswordHash = "password123", FirstName = "Mia", MiddleName = "Alegre", LastName = "Fulgueras" }
            );

            modelBuilder.Entity<Network>().HasData(
                new Network { Id = 1, Name = "Youth" },
                new Network { Id = 2, Name = "Women" },
                new Network { Id = 3, Name = "Men" },
                new Network { Id = 4, Name = "Children" },
                new Network { Id = 5, Name = "YAN" }
            );
        }
    }
}
