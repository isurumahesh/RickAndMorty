using MediatR;
using RickAndMorty.Application.Commands;
using RickAndMorty.Infrastructure;

namespace RickAndMorty.DataProcessor
{
    public class ApplicationService
    {
        private readonly ICharacterService characterService;
        private readonly IMediator mediator;

        public ApplicationService(ICharacterService characterService, IMediator mediator)
        {
            this.characterService = characterService;
            this.mediator = mediator;
        }

        public async Task ReadData()
        {
            Console.WriteLine("Start reading Character data");
            var characters = await characterService.ReadCharacterData();
            await mediator.Send(new SaveCharactersCommand(characters));

            Console.WriteLine("Character data reading is complted");
        }
    }
}