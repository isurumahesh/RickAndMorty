using AutoMapper;
using MediatR;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

public record SaveLocationsCommand(List<LocationDTO> LocationList) : IRequest;

public class SaveLocationCommandHandler
: IRequestHandler<SaveLocationsCommand>
{
    private readonly ILocationRepository locationRepository;
    private readonly IMapper mapper;

    public SaveLocationCommandHandler(ILocationRepository locationRepository, IMapper mapper)
    {
        this.locationRepository = locationRepository;    
        this.mapper = mapper;
    }

    public async Task Handle(SaveLocationsCommand request, CancellationToken cancellationToken)
    {       
        var locations = mapper.Map<List<Location>>(request.LocationList);
        await locationRepository.SaveLocations(locations);
    }
}