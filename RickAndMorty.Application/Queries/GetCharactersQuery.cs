using AutoMapper;
using MediatR;
using RickAndMorty.Application.Constants;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.Application.Queries
{
    public record GetCharactersQuery() : IRequest<GetCharactersResponse>;
    public record GetCharactersResponse(List<CharacterDTO> Characters, bool IsFromCache);

    public class GetCharacterQueryHandler : IRequestHandler<GetCharactersQuery, GetCharactersResponse>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public GetCharacterQueryHandler(ICharacterRepository characterRepository, ICacheService cacheService, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<GetCharactersResponse> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            var cachedCharacters = cacheService.Get<List<CharacterDTO>>(CacheConstants.CharacterList);
            if (cachedCharacters != null)
            {
                return new(cachedCharacters, true);
            }

            var characters = await characterRepository.GetCharacters();

            var mappedCharacters = mapper.Map<List<CharacterDTO>>(characters);
            cacheService.Set(CacheConstants.CharacterList, mappedCharacters, TimeSpan.FromMinutes(5));

            return new(mappedCharacters.OrderByDescending(a => a.Id).ToList(), false);
        }
    }
}