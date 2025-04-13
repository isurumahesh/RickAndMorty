using RickAndMorty.Application.DTOs;

namespace RickAndMorty.Infrastructure.Services
{
    public interface IApiDataReadService
    {
        Task<List<CharacterDTO>> ReadCharacterData();

        Task<List<LocationDTO>> ReadLocationData(List<string> locationUrls);
    }
}