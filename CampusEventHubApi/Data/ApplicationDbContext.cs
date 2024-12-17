using Microsoft.EntityFrameworkCore;
using CampusEventHubApi.Models;


namespace CampusEventHubApi.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Event> Event { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("User")
                .Property(u => u.IsAdmin)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .ToTable("User")
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Event>()
                .ToTable("Event")
                .Property(e => e.Approved)
                .HasDefaultValue(false);

            modelBuilder.Entity<Event>()
                .ToTable("Event")
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }


    }
}
