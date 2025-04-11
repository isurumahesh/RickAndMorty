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
        public DbSet<Origin> Origins { get; set; }

    }
}