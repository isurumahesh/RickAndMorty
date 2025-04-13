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
    public record SaveCharacterCommand(CharacterSaveDTO characterDTO) : IRequest;

    public class SaveCharacterCommandHandler : IRequestHandler<SaveCharacterCommand>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ICacheService cacheService;
        private readonly ILogger<SaveCharacterCommandHandler> logger;
        private readonly IMapper mapper;

        public SaveCharacterCommandHandler(ICharacterRepository characterRepository, ICacheService caheService, ILogger<SaveCharacterCommandHandler> logger, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.cacheService = caheService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task Handle(SaveCharacterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var character = mapper.Map<Character>(request.characterDTO);
                await characterRepository.SaveCharacter(character);
                cacheService.Remove(CacheConstants.CharacterList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while saving character");
                throw;
            }
        }
    }
}