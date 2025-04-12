using RickAndMorty.Application.DTOs;

namespace RickAndMorty.Infrastructure.Services
{
    public interface ICharacterService
    {
        Task<List<CharacterDTO>> ReadCharacterData();
    }
}