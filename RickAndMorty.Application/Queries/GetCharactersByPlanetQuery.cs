using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.Application.Queries
{
    public record GetCharactersByPlanetQuery(string PlanetName) : IRequest<List<CharacterDTO>>;

    public class GetCharactersByPlanetQueryHandler : IRequestHandler<GetCharactersByPlanetQuery, List<CharacterDTO>>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ILogger<GetCharactersByPlanetQueryHandler> logger;
        private readonly IMapper mapper;

        public GetCharactersByPlanetQueryHandler(ICharacterRepository characterRepository, ILogger<GetCharactersByPlanetQueryHandler> logger, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<List<CharacterDTO>> Handle(GetCharactersByPlanetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var characters = await characterRepository.GetCharactersByPlanet(request.PlanetName);
                var mappedCharacters = mapper.Map<List<CharacterDTO>>(characters);
                return mappedCharacters;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while retrieving characters by planet");
                throw;
            }
        }
    }
}