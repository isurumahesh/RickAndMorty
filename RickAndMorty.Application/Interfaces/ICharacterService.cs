using RickAndMorty.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Application.Interfaces
{
    public interface ICharacterService
    {
        Task AddCharacterData(CharacterDTO character);
    }
}
