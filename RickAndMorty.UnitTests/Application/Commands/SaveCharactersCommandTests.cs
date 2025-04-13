using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using RickAndMorty.Application;
using RickAndMorty.Application.Commands;
using RickAndMorty.Application.Constants;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.UnitTests.Application.Commands
{
    public class SaveCharactersCommandTests
    {
        private readonly Mock<ICharacterRepository> mockCharacterRepository;
        private readonly Mock<ILocationRepository> mockLocationRepository;
        private readonly Mock<ICacheService> mockCacheService;
        private readonly Mock<ILogger<SaveCharactersCommandHandler>> mockLogger;
        private readonly IMapper mapper;

        public SaveCharactersCommandTests()
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
            mockCharacterRepository = new Mock<ICharacterRepository>();
            mockCacheService = new Mock<ICacheService>();
            mockLogger = new Mock<ILogger<SaveCharactersCommandHandler>>();
        }

        [Fact]
        public async Task CharactersShouldBeSavedToDatabase()
        {
            var characterCommand = new SaveCharactersCommandHandler(mockCharacterRepository.Object, mockLocationRepository.Object, mockCacheService.Object, mockLogger.Object, mapper);
            var command = new SaveCharactersCommand(new List<CharacterDTO>());
            mockCharacterRepository.Setup(r => r.SaveCharacters(It.IsAny<List<Character>>())).Returns(Task.CompletedTask);
            mockLocationRepository.Setup(l => l.GetLocations()).ReturnsAsync(new List<Location>());
            mockCacheService.Setup(c => c.Remove(CacheConstants.CharacterList)).Verifiable();

            await characterCommand.Handle(command, CancellationToken.None);

            mockCacheService.Verify(c => c.Remove(CacheConstants.CharacterList), Times.Once);
            mockCharacterRepository.Verify(r => r.SaveCharacters(It.IsAny<List<Character>>()), Times.Once);
            mockLocationRepository.Verify(r => r.GetLocations(), Times.Once);
        }
    }
}