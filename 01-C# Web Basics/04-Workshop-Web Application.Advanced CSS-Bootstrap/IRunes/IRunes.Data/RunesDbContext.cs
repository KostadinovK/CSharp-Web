using IRunes.Models.Models;

namespace IRunes.Data
{
    using Microsoft.EntityFrameworkCore;

    public class RunesDbContext : DbContext
    {
        public RunesDbContext() : base()
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionConfiguration.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Track>()
                .HasOne(t => t.Album)
                .WithMany(a => a.Tracks)
                .HasForeignKey(t => t.AlbumId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

