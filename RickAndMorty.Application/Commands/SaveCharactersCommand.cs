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
        private readonly IMapper mapper;

        public SaveCharacterCommandHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.mapper = mapper;
        }

        public async Task Handle(SaveCharactersCommand request, CancellationToken cancellationToken)
        {
            var characters = mapper.Map<List<Character>>(request.Characters);
            await characterRepository.SaveCharacters(characters);
        }
    }
}