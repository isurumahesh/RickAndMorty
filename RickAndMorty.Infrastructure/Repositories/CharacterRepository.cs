using Microsoft.EntityFrameworkCore;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Data;

namespace RickAndMorty.Infrastructure.Repositories
{
    public class CharacterRepository(RickAndMortyDbContext dbContext) : ICharacterRepository
    {
        public async Task<List<Character>> GetCharacters()
        {
            return await dbContext.Characters
                .Include(a => a.Origin)
                .Include(a => a.Location)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveCharacter(Character character)
        {
            try
            {
                var maxId = await dbContext.Characters.MaxAsync(a => a.Id);
                character.Id = maxId + 1;
                await dbContext.Characters.AddAsync(character);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveCharacters(List<Character> characters)
        {
            await dbContext.Characters.AddRangeAsync(characters);
            await dbContext.SaveChangesAsync();
        }
    }
}