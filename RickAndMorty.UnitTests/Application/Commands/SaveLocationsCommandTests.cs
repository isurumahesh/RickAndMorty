using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using RickAndMorty.Application;
using RickAndMorty.Application.Commands;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.UnitTests.Application.Commands
{
    public class SaveLocationsCommandTests
    {
        private readonly Mock<ILocationRepository> mockLocationRepository;
        private readonly Mock<ILogger<SaveLocationCommandHandler>> mockLogger;
        private readonly IMapper mapper;

        public SaveLocationsCommandTests()
        {
            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                this.mapper = mapper;
            }

            mockLocationRepository = new Mock<ILocationRepository>();

            mockLogger = new Mock<ILogger<SaveLocationCommandHandler>>();
        }

        [Fact]
        public async Task LocationsShouldBeSavedToDatabase()
        {
            var locationCommandHandler = new SaveLocationCommandHandler(mockLocationRepository.Object, mockLogger.Object, mapper);
            var command = new SaveLocationsCommand(new List<LocationDTO>());
            mockLocationRepository.Setup(r => r.SaveLocations(It.IsAny<List<Location>>())).Returns(Task.CompletedTask);

            await locationCommandHandler.Handle(command, CancellationToken.None);

            mockLocationRepository.Verify(r => r.SaveLocations(It.IsAny<List<Location>>()), Times.Once);
        }
    }
}