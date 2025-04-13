using RickAndMorty.Core.Entities;

namespace RickAndMorty.Core.Interfaces
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetCharacters();

        Task<List<Character>> GetCharactersByPlanet(string planetName);

        Task SaveCharacter(Character character);

        Task SaveCharacters(List<Character> characters);
    }
}