using AutoMapper;
using MediatR;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.Application.Commands
{
    public record SaveCharactersCommand(List<CharacterDTO> Characters) : IRequest;

    public class SaveCharacterCommandHandler
      : IRequestHandler<SaveCharactersCommand>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IOriginRepository originRepository;
        private readonly ICleanDatabaseService cleanDatabaseService;
        private readonly IMapper mapper;

        public SaveCharacterCommandHandler(ICharacterRepository characterRepository, IOriginRepository originRepository,
            ILocationRepository locationRepository, ICleanDatabaseService cleanDatabaseService, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.originRepository = originRepository;
            this.locationRepository = locationRepository;
            this.cleanDatabaseService = cleanDatabaseService;
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

                await characterRepository.ClearAndSaveCharacters(characters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}