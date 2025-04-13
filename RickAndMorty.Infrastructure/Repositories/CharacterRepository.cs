using Microsoft.EntityFrameworkCore;
using RickAndMorty.Application.Constants;
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

        public async Task<List<Character>> GetCharactersByPlanet(string planetName)
        {
            return await dbContext.Characters
                    .Where(a => a.Origin != null && a.Origin.Type == CharacterConstants.OriginType && a.Origin.Name.Replace(" ", "").ToLower() == planetName)
                    .Include(a => a.Origin)
                    .Include(a => a.Location)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task SaveCharacter(Character character)
        {
            var maxId = await dbContext.Characters.AnyAsync() ? await dbContext.Characters.MaxAsync(a => a.Id) : 0;
            character.Id = maxId + 1;
            await dbContext.Characters.AddAsync(character);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveCharacters(List<Character> characters)
        {
            foreach (var character in characters)
            {
                character.Origin = null;
                character.Location = null;
            }

            await dbContext.Characters.AddRangeAsync(characters);
            await dbContext.SaveChangesAsync();
        }
    }
}