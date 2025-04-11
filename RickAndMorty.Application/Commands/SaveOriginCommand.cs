using AutoMapper;
using MediatR;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

public record SaveOriginCommand(OriginDTO originDTO) : IRequest;

public class SaveOriginCommandHandler
: IRequestHandler<SaveOriginCommand>
{
    private readonly IOriginRepository originRepository;
    private readonly IMapper mapper;

    public SaveOriginCommandHandler(IOriginRepository originRepository, IMapper mapper)
    {
        this.originRepository = originRepository;
        this.mapper = mapper;
    }

    public async Task Handle(SaveOriginCommand request, CancellationToken cancellationToken)
    {
        var characters = mapper.Map<Origin>(request.originDTO);
        await originRepository.SaveOrigin(characters);
    }
}