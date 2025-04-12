using Microsoft.EntityFrameworkCore;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Data;

namespace RickAndMorty.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly RickAndMortyDbContext dbContext;

        public LocationRepository(RickAndMortyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Location> GetLocation(string url)
        {
            var location = await dbContext.Locations.FirstOrDefaultAsync(a => a.Url == url);
            return location;
        }

        public async Task<Location> SaveLocation(Location location)
        {
            await dbContext.Locations.AddAsync(location);
            await dbContext.SaveChangesAsync();
            return location;
        }
    }
}