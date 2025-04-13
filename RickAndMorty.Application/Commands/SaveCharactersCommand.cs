using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RickAndMorty.Application.Constants;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.Application.Commands
{
    public record SaveCharactersCommand(List<CharacterDTO> Characters) : IRequest;

    public class SaveCharactersCommandHandler : IRequestHandler<SaveCharactersCommand>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ILocationRepository locationRepository;
        private readonly ICacheService cacheService;
        private readonly ILogger<SaveCharactersCommandHandler> logger;
        private readonly IMapper mapper;

        public SaveCharactersCommandHandler(ICharacterRepository characterRepository,
            ILocationRepository locationRepository, ICacheService cacheService, ILogger<SaveCharactersCommandHandler> logger, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.locationRepository = locationRepository;
            this.cacheService = cacheService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task Handle(SaveCharactersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var characters = mapper.Map<List<Character>>(request.Characters);
                var locations = await locationRepository.GetLocations();

                foreach (var character in characters)
                {
                    var existingOrigin = locations.FirstOrDefault(a => a.Url == character?.Origin?.Url);
                    character.OriginId = existingOrigin?.Id;

                    var existingLocation = locations.FirstOrDefault(a => a.Url == character?.Location?.Url);
                    character.LocationId = existingLocation?.Id;
                }

                await characterRepository.SaveCharacters(characters);
                cacheService.Remove(CacheConstants.CharacterList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while saving characters");
                throw;
            }
        }
    }
}