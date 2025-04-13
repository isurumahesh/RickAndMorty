using Microsoft.EntityFrameworkCore;
using RickAndMorty.Core.Entities;

namespace RickAndMorty.Infrastructure.Data
{
    public class RickAndMortyDbContext : DbContext
    {
        public RickAndMortyDbContext(DbContextOptions<RickAndMortyDbContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Status).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Species).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Type).HasMaxLength(50);
                entity.Property(c => c.Gender).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Dimension).HasMaxLength(100);
                entity.Property(c => c.Type).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Url).IsRequired();
                entity.HasIndex(c => c.Type);
            });

            modelBuilder.Entity<Character>()
                .HasOne(c => c.Origin)
                .WithMany(p => p.OriginCharacters)
                .HasForeignKey(c => c.OriginId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Character>()
                .HasOne(c => c.Location)
                .WithMany(p => p.LocationCharacters)
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}