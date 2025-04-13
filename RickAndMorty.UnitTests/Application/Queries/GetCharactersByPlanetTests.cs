using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using RickAndMorty.Application;
using RickAndMorty.Application.Queries;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;

namespace RickAndMorty.UnitTests.Application.Queries
{
    public class GetCharactersByPlanetTests
    {
        private readonly Mock<ICharacterRepository> mockCharacterRepository;
        private readonly Mock<ILogger<GetCharactersByPlanetQueryHandler>> mockLogger;
        private readonly IMapper mapper;
        private const string PlanetName = "pluto";

        public GetCharactersByPlanetTests()
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
            mockLogger = new Mock<ILogger<GetCharactersByPlanetQueryHandler>>();
        }

        [Fact]
        public async Task CharacterListShouldBeReturnedFromDatabase()
        {
            var charactersQuery = new GetCharactersByPlanetQueryHandler(mockCharacterRepository.Object, mockLogger.Object, mapper);

            mockCharacterRepository.Setup(x => x.GetCharactersByPlanet(It.IsAny<string>()))
               .ReturnsAsync(GetCharactersList());

            var result = await charactersQuery.Handle(new GetCharactersByPlanetQuery(PlanetName), new CancellationToken());

            Assert.True(result.Any());
            Assert.Equal(1, result.Count);
        }

        private List<Character> GetCharactersList()
        {
            return new List<Character>
            {
                new Character
                {
                    Name = "Test1",
                    Origin = new Location
                    {
                        Type = "Planet",
                        Name = "pluto"
                    }
                },
            };
        }
    }
}