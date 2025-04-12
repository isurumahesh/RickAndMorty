using RickAndMorty.Core.Entities;

namespace RickAndMorty.Core.Interfaces
{
    public interface IOriginRepository
    {
        Task<Origin> GetOrigin(string url);

        Task<Origin> SaveOrigin(Origin origin);
    }
}