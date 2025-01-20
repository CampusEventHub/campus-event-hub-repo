using Microsoft.EntityFrameworkCore;
using CampusEventHubApi.Models;

namespace CampusEventHubApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Kalendar> Kalendari { get; set; } // Dodano za tablicu Kalendar
        public DbSet<Ocjena> Ocjene { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracija za tablicu User
            modelBuilder.Entity<User>()
                .ToTable("User")
                .Property(u => u.IsAdmin)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .ToTable("User")
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            // Konfiguracija za tablicu Event
            modelBuilder.Entity<Event>()
                .ToTable("Event")
                .Property(e => e.Approved)
                .HasDefaultValue(false);

            modelBuilder.Entity<Event>()
                .ToTable("Event")
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            // Konfiguracija za tablicu Kalendar
            modelBuilder.Entity<Kalendar>()
                .ToTable("Kalendar") // Naziv tablice u bazi
                .Property(k => k.Kreirano)
                .HasDefaultValueSql("GETDATE()"); // Zadana vrijednost za Kreirano
        }
    }
}
