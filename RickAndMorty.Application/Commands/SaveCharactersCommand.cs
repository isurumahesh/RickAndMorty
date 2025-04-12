using AutoMapper;
using MediatR;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.Application.Commands
{
    public record SaveCharactersCommand(List<CharacterDTO> Characters) : IRequest;

    public class SaveCharactersCommandHandler
      : IRequestHandler<SaveCharactersCommand>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IOriginRepository originRepository;
        private readonly ICleanDatabaseService cleanDatabaseService;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public SaveCharactersCommandHandler(ICharacterRepository characterRepository, IOriginRepository originRepository,
            ILocationRepository locationRepository, ICleanDatabaseService cleanDatabaseService,ICacheService cacheService, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.originRepository = originRepository;
            this.locationRepository = locationRepository;
            this.cleanDatabaseService = cleanDatabaseService;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task Handle(SaveCharactersCommand request, CancellationToken cancellationToken)
        {
            await cleanDatabaseService.CleanDatabase();
            var characters = mapper.Map<List<Character>>(request.Characters);

            try
            {
                foreach (var character in characters)
                {
                    var existingOrigin = await originRepository.GetOrigin(character.Origin.Url);

                    if (existingOrigin == null)
                    {
                        var newOrigin = await originRepository.SaveOrigin(character.Origin);
                        character.Origin = newOrigin;
                    }
                    else
                    {
                        character.Origin = existingOrigin;
                    }

                    var existingLocation = await locationRepository.GetLocation(character.Location.Url);

                    if (existingLocation == null)
                    {
                        var location = await locationRepository.SaveLocation(character.Location);
                        character.Location = location;
                    }
                    else
                    {
                        character.Location = existingLocation;
                    }
                }

                await characterRepository.SaveCharacters(characters);
                cacheService.Remove("CharacterList");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}