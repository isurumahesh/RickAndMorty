using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Application.Constants;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Infrastructure.Data;
using System.Net.Http.Json;

namespace RickAndMorty.IntegrationTests
{
    public class CharactersControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;

        public CharactersControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _httpClient = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task ShouldReturnCharactersListFromDatabase()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedSevices = scope.ServiceProvider;
                var db = scopedSevices.GetRequiredService<RickAndMortyDbContext>();

                db.Database.EnsureCreated();
                Seeding.IntializeTestDb(db);
            }

            var response = await _httpClient.GetAsync("api/characters");
            var result = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().HaveCount(3);
            response.Headers.Should().Contain(header => header.Key == "X-From-Database");

            var headerValue = response.Headers.GetValues("X-From-Database").FirstOrDefault();
            headerValue.Should().NotBeNull();
            headerValue.Should().Be("Yes");
        }

        [Fact]
        public async Task ShouldReturnCharactersListFromCache()
        {
            _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddMemoryCache();
                });
            });

            using (var scope = _factory.Services.CreateScope())
            {
                var scopedSevices = scope.ServiceProvider;
                var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

                var expectedCharacters = new List<CharacterDTO>
                    {
                    new CharacterDTO { Id = 1, Name = "Rick Sanchez" },
                    new CharacterDTO { Id = 2, Name = "Morty Smith" }
                    };

                memoryCache.Set(CacheConstants.CharacterList, expectedCharacters, TimeSpan.FromMinutes(5));
            }

            var response = await _httpClient.GetAsync("api/characters");
            var result = await response.Content.ReadFromJsonAsync<List<CharacterDTO>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().HaveCount(2);
            response.Headers.Should().Contain(header => header.Key == "X-From-Database");

            var headerValue = response.Headers.GetValues("X-From-Database").FirstOrDefault();
            headerValue.Should().NotBeNull();
            headerValue.Should().Be("No");
        }
    }
}