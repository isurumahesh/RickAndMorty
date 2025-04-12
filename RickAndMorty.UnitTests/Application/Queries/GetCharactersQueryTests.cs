using AutoMapper;
using Moq;
using RickAndMorty.Application;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Application.Queries;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.UnitTests.Application.Queries
{
    public class GetCharactersQueryTests
    {
        private readonly Mock<ICharacterRepository> mockCharacterRepository;
        private readonly Mock<ICacheService> mockCacheService;
        private readonly IMapper mapper;

        public GetCharactersQueryTests()
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
        public async Task CharacterListShouldBeReturnedFromCache()
        {
            var charactersQuery = new GetCharacterQueryHandler(mockCharacterRepository.Object, mockCacheService.Object, mapper);
            mockCacheService.Setup(x => x.Get<List<CharacterDTO>>(It.IsAny<string>()))
                .Returns(GetCharactersDTOList());

            var result = await charactersQuery.Handle(new GetCharactersQuery(), new CancellationToken());

            Assert.True(result.IsFromCache);
            Assert.True(result.Characters.Any());
        }

        [Fact]
        public async Task CharacterListShouldBeReturnedFromDatabase()
        {
            var charactersQuery = new GetCharacterQueryHandler(mockCharacterRepository.Object, mockCacheService.Object, mapper);
            mockCacheService.Setup(x => x.Get<List<CharacterDTO>>(It.IsAny<string>()))
                .Returns(() => null);
            mockCharacterRepository.Setup(x => x.GetCharacters())
               .ReturnsAsync(GetCharactersList());

            var result = await charactersQuery.Handle(new GetCharactersQuery(), new CancellationToken());

            Assert.False(result.IsFromCache);
            Assert.True(result.Characters.Any());
        }

        private List<CharacterDTO> GetCharactersDTOList()
        {
            return new List<CharacterDTO>
            {
                new CharacterDTO
                {
                    Name = "Test1",
                },
                new CharacterDTO
                {
                    Name="Test2"
                }
            };
        }

        private List<Character> GetCharactersList()
        {
            return new List<Character>
            {
                new Character
                {
                    Name = "Test1",
                },
                new Character
                {
                    Name="Test2"
                }
            };
        }
    }
}