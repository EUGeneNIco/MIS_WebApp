
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MIS.Application._Helpers;
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
                new User { Id = 1, UserName = "admin@mis", Role = "Admin", PasswordHash = PasswordHelper.Hash("jesusislord"), FirstName = "Admin", MiddleName = "", LastName = "" },
                new User { Id = 2, UserName = "mia@mis", Role = "Admin", PasswordHash = PasswordHelper.Hash("password123"), FirstName = "Mia", MiddleName = "Alegre", LastName = "Fulgueras" },
                new User { Id = 3, UserName = "staff@mis", Role = "Staff", PasswordHash = PasswordHelper.Hash("jesusislord"), FirstName = "Staff", MiddleName = "", LastName = "" }
            );

            modelBuilder.Entity<Network>().HasData(
                new Network { Id = 1, Name = "KKB/CYN" },
                new Network { Id = 2, Name = "Women" },
                new Network { Id = 3, Name = "Men" },
                new Network { Id = 4, Name = "Children" },
                new Network { Id = 5, Name = "Y-AM" }
            );
        }
    }
}
