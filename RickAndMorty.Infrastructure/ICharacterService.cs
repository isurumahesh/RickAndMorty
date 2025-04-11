using RickAndMorty.Application.DTOs;

namespace RickAndMorty.Infrastructure
{
    public interface ICharacterService
    {
        Task<List<CharacterDTO>> ReadCharacterData();
    }
}