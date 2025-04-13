using Microsoft.EntityFrameworkCore;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Data;
using System;

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
            var location = await dbContext.Locations.AsNoTracking().FirstOrDefaultAsync(a => a.Url == url);
            return location;
        }

        public async Task<List<Location>> GetLocations()
        {
            var locations = await dbContext.Locations.AsNoTracking().ToListAsync();
            return locations;
        }

        public async Task SaveLocations(List<Location> locations)
        {
            try
            {
                await dbContext.Locations.AddRangeAsync(locations);
                await dbContext.SaveChangesAsync();               
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}