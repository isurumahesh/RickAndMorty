using RickAndMorty.Core.Entities;

namespace RickAndMorty.Core.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location> GetLocation(string url);
        Task<List<Location>> GetLocations();

        Task SaveLocations(List<Location> locations);

    }
}