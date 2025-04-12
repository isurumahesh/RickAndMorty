using AutoMapper;
using MediatR;
using RickAndMorty.Application.Constants;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.Application.Commands
{
    public record SaveCharacterCommand(CharacterSaveDTO characterDTO) : IRequest;

    public class SaveCharacterCommandHandler : IRequestHandler<SaveCharacterCommand>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public SaveCharacterCommandHandler(ICharacterRepository characterRepository, ICacheService caheService, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.cacheService = caheService;
            this.mapper = mapper;
        }

        public async Task Handle(SaveCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = mapper.Map<Character>(request.characterDTO);
            await characterRepository.SaveCharacter(character);
            cacheService.Remove(CacheConstants.CharacterList);
        }
    }
}