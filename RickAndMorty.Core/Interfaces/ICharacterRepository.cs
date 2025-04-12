using RickAndMorty.Core.Entities;

namespace RickAndMorty.Core.Interfaces
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetCharacters();

        Task SaveCharacter(Character character);

        Task SaveCharacters(List<Character> characters);
    }
}