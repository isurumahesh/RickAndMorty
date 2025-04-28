using Azure.Messaging.ServiceBus;
using RickAndMorty.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RickAndMorty.Infrastructure.Services
{
    public class CharacterService:Application.Interfaces.ICharacterService
    {
        private readonly ServiceBusClient _serviceBusClient;
        public CharacterService(ServiceBusClient serviceBusClient)
        {

            _serviceBusClient = serviceBusClient;

        }
        public async Task AddCharacterData(CharacterDTO character)
        {

         //   var client = new ServiceBusClient(connectionString);
            var sender = _serviceBusClient.CreateSender("add-character-data");
            var body= JsonSerializer.Serialize(character);
            var message = new ServiceBusMessage(body);

            if (character.Id == 826) {
                message.ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(15); 
            }
          
            await sender.SendMessageAsync(message);

        }
    }
}
