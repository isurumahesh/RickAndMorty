using RickAndMorty.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Interfaces
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetCharacters();
        Task SaveCharacter(Character character);
        Task SaveCharacters(List<Character> characters);

    }
}
