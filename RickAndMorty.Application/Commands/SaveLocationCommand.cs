using AutoMapper;
using MediatR;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Application.Commands
{
    public record SaveLocationCommand(LocationDTO locationDTO):IRequest;

    public class SaveLocationCommandHandler
  : IRequestHandler<SaveLocationCommand>
    {
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;

        public SaveLocationCommandHandler(ILocationRepository locationRepository, IMapper mapper)
        {
            this.locationRepository = locationRepository;
            this.mapper = mapper;
        }

        public async Task Handle(SaveLocationCommand request, CancellationToken cancellationToken)
        {
            var characters = mapper.Map<Location>(request.locationDTO);
            await locationRepository.SaveLocation(characters);
        }
    }
}
