using AutoMapper;
using MediatR;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.Application.Queries
{
    public record GetCharactersQuery() : IRequest<List<CharacterDTO>>;

    public class GetCharacterQueryHandler : IRequestHandler<GetCharactersQuery, List<CharacterDTO>>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IMapper mapper;

        public GetCharacterQueryHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.mapper = mapper;
        }

        public async Task<List<CharacterDTO>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            var characters = await characterRepository.GetCharacters();
            var mappedCharacters = mapper.Map<List<CharacterDTO>>(characters);
            return mappedCharacters;
        }
    }
}