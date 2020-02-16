using SharedTrip.Models;

namespace SharedTrip
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UserTrip { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserTrip>()
                .HasKey(ut => new { ut.UserId, ut.TripId });

            modelBuilder
                .Entity<UserTrip>()
                .HasOne(ut => ut.User)
                .WithMany(ut => ut.UserTrips)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<UserTrip>()
                .HasOne(ut => ut.Trip)
                .WithMany(ut => ut.UserTrips)
                .HasForeignKey(ut => ut.TripId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
