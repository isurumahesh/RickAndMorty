using MediatR;
using Microsoft.Extensions.Logging;
using RickAndMorty.Application.Commands;
using RickAndMorty.Infrastructure;

namespace RickAndMorty.DataProcessor
{
    public class ApplicationService
    {
        private readonly ICharacterService characterService;
        private readonly IMediator mediator;
        private readonly ILogger<ApplicationService> _logger;

        public ApplicationService(ICharacterService characterService, IMediator mediator, ILogger<ApplicationService> logger)
        {
            this.characterService = characterService;
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task ReadData()
        {
            _logger.LogInformation("Starting to read data from the API...");

            Console.WriteLine("Start reading Character data");
            var characters = await characterService.ReadCharacterData();
            await mediator.Send(new SaveCharactersCommand(characters));

            Console.WriteLine("Character data reading is complted");
        }
    }
}