using RickAndMorty.Application.DTOs;
using System.Net.Http.Json;

namespace RickAndMorty.Infrastructure.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _httpClient;

        public CharacterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CharacterDTO>> ReadCharacterData()
        {
            Console.WriteLine("Data fetched");
            var nextPage = "character";
            var aliveCharactersList = new List<CharacterDTO>();

            try
            {
                while (nextPage != null)
                {
                    var response = await _httpClient.GetFromJsonAsync<ApiResponse<CharacterDTO>>(nextPage);
                    var aliveCharacters = response?.Results.Where(a => a.Status == "Alive").ToList();
                    aliveCharactersList.AddRange(aliveCharacters);

                    nextPage = response?.Info.Next;
                }

                return aliveCharactersList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data reading error");
                return new List<CharacterDTO>();
            }
        }
    }
}