using RickAndMorty.Core.Entities;

namespace RickAndMorty.Core.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location> GetLocation(string url);

        Task<Location> SaveLocation(Location origin);
    }
}