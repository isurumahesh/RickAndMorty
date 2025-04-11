using Microsoft.EntityFrameworkCore;
using Polly;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Infrastructure.Repositories
{
    public class CharacterRepository(RickAndMortyDbContext dbContext) : ICharacterRepository
    {
        public async Task<List<Character>> GetCharacters()
        {
            return await dbContext.Characters.Include(a => a.Origin).Include(a => a.Location).ToListAsync();
        }

        public async Task SaveCharacter(Character character)
        {
            await dbContext.Characters.AddAsync(character);
            await dbContext.SaveChangesAsync();
        }

        public async Task ClearAndSaveCharacters(List<Character> characters)
        {
           // dbContext.Characters.RemoveRange(dbContext.Characters);
           // await dbContext.SaveChangesAsync();

            await dbContext.Characters.AddRangeAsync(characters);
            await dbContext.SaveChangesAsync();
        }
    }
}