using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Data;

namespace RickAndMorty.Infrastructure.Services
{
    public class CleanDatabaseService : ICleanDatabaseService
    {
        private readonly RickAndMortyDbContext dbContext;

        public CleanDatabaseService(RickAndMortyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CleanDatabase()
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var characters = dbContext.Characters.ToList();
                var origins = dbContext.Origins.ToList();
                var locations = dbContext.Locations.ToList();

                dbContext.Characters.RemoveRange(characters);
                dbContext.Origins.RemoveRange(origins);
                dbContext.Locations.RemoveRange(locations);

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}