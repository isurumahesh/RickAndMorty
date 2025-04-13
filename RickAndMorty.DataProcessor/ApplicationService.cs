using MediatR;
using Microsoft.Extensions.Logging;
using RickAndMorty.Application.Commands;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Services;

namespace RickAndMorty.DataProcessor
{
    public class ApplicationService
    {
        private readonly IApiDataReadService apiDataReadService;
        private readonly IMediator mediator;
        private readonly ICleanDatabaseService cleanDatabaseService;
        private readonly ILogger<ApplicationService> logger;

        public ApplicationService(IApiDataReadService apiDataReadService, IMediator mediator, ICleanDatabaseService cleanDatabaseService, ILogger<ApplicationService> logger)
        {
            this.apiDataReadService = apiDataReadService;
            this.mediator = mediator;
            this.cleanDatabaseService = cleanDatabaseService;
            logger = logger;
        }

        public async Task ReadData()
        {
            try
            {
                Console.WriteLine("Started reading Character data");

                var characters = await apiDataReadService.ReadCharacterData();

                var originUrlList = characters.Where(c => !String.IsNullOrEmpty(c.Origin.Url)).Select(c => c.Origin.Url).ToList();
                var locationUrlList = characters.Where(c => !String.IsNullOrEmpty(c.Location.Url)).Select(c => c.Location.Url).ToList();

                var mergedList = originUrlList.Union(locationUrlList).ToList();

                var locationList = await apiDataReadService.ReadLocationData(mergedList);

                await cleanDatabaseService.CleanDatabase();

                await mediator.Send(new SaveLocationsCommand(locationList));
                await mediator.Send(new SaveCharactersCommand(characters));

                Console.WriteLine("Completed reading Character data");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error reading character data");               
            }
        }
    }
}