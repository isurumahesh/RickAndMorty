using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RickAndMorty.Application.Constants;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.Application.Queries
{
    public record GetCharactersQuery() : IRequest<GetCharactersResponse>;
    public record GetCharactersResponse(List<CharacterDTO> Characters, bool IsFromCache);

    public class GetCharactersQueryHandler : IRequestHandler<GetCharactersQuery, GetCharactersResponse>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ICacheService cacheService;
        private readonly ILogger<GetCharactersQueryHandler> logger;
        private readonly IMapper mapper;

        public GetCharactersQueryHandler(ICharacterRepository characterRepository, ICacheService cacheService, ILogger<GetCharactersQueryHandler> logger, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.cacheService = cacheService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<GetCharactersResponse> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cachedCharacters = cacheService.Get<List<CharacterDTO>>(CacheConstants.CharacterList);
                if (cachedCharacters is not null)
                {
                    return new(cachedCharacters, true);
                }

                var characters = await characterRepository.GetCharacters();

                var mappedCharacters = mapper.Map<List<CharacterDTO>>(characters);
                var orderedCharacters = mappedCharacters.OrderByDescending(a => a.Id).ToList();
                cacheService.Set(CacheConstants.CharacterList, orderedCharacters, TimeSpan.FromMinutes(5));

                return new(orderedCharacters, false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving character");
                throw;
            }
        }
    }
}