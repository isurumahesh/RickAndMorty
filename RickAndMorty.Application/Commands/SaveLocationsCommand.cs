using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

public record SaveLocationsCommand(List<LocationDTO> LocationList) : IRequest;

public class SaveLocationCommandHandler: IRequestHandler<SaveLocationsCommand>
{
    private readonly ILocationRepository locationRepository;
    private readonly ILogger<SaveLocationCommandHandler> logger;
    private readonly IMapper mapper;

    public SaveLocationCommandHandler(ILocationRepository locationRepository,ILogger<SaveLocationCommandHandler> logger, IMapper mapper)
    {
        this.locationRepository = locationRepository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task Handle(SaveLocationsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var locations = mapper.Map<List<Location>>(request.LocationList);
            await locationRepository.SaveLocations(locations);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while saving locations");
            throw;
        }
    }
}