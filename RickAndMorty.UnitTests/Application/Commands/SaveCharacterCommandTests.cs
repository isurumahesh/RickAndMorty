using AutoMapper;
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
    public class SaveCharacterCommandTests
    {
        private readonly Mock<ICharacterRepository> mockCharacterRepository;
        private readonly Mock<ICacheService> mockCacheService;
        private readonly IMapper mapper;

        public SaveCharacterCommandTests()
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

            mockCharacterRepository = new Mock<ICharacterRepository>();
            mockCacheService = new Mock<ICacheService>();
        }

        [Fact]
        public async Task CharacterShouldBeSavedToDatabase()
        {
            var characterCommand = new SaveCharacterCommandHandler(mockCharacterRepository.Object, mockCacheService.Object, mapper);
            var command = new SaveCharacterCommand(new CharacterSaveDTO());
            mockCharacterRepository.Setup(r => r.SaveCharacter(It.IsAny<Character>())).Returns(Task.CompletedTask);
            mockCacheService.Setup(c => c.Remove(CacheConstants.CharacterList)).Verifiable();

            await characterCommand.Handle(command, CancellationToken.None);

            mockCacheService.Verify(c => c.Remove(CacheConstants.CharacterList), Times.Once);
            mockCharacterRepository.Verify(r => r.SaveCharacter(It.IsAny<Character>()), Times.Once);
        }
    }
}