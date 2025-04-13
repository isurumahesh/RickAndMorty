using RickAndMorty.Application.Constants;
using RickAndMorty.Application.DTOs;
using System.Net.Http.Json;

namespace RickAndMorty.Infrastructure.Services
{
    public class ApiDataReadService : IApiDataReadService
    {
        private readonly HttpClient _httpClient;

        public ApiDataReadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CharacterDTO>> ReadCharacterData()
        {           
            var nextPage = "character";
            var aliveCharactersList = new List<CharacterDTO>();

            try
            {
                while (nextPage != null)
                {
                    var response = await _httpClient.GetFromJsonAsync<ApiResponse<CharacterDTO>>(nextPage);
                    var aliveCharacters = response?.Results.Where(a => a.Status == CharacterConstants.StatusAlive).ToList();
                    aliveCharactersList.AddRange(aliveCharacters);

                    nextPage = response?.Info.Next;
                }

                return aliveCharactersList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data reading error from character api");
                throw;
            }
        }

        public async Task<List<LocationDTO>> ReadLocationData(List<string> locationUrls)
        {
            var locationList = new List<LocationDTO>();
            try
            {
                foreach (var locationUrl in locationUrls)
                {
                    var response = await _httpClient.GetFromJsonAsync<LocationDTO>(locationUrl);
                    locationList.Add(response);
                }

                return locationList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data reading error from location api");
                throw;
            }
        }
    }
}