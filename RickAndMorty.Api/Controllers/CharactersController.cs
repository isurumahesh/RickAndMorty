using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Application.Commands;
using RickAndMorty.Application.Constants;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Application.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RickAndMorty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IValidator<CharacterSaveDTO> validator;
        private readonly ICharacterService characterService;

        public CharactersController(IMediator mediator, IValidator<CharacterSaveDTO> validator, ICharacterService characterService)
        {
            this.mediator = mediator;
            this.validator = validator;
            this.characterService = characterService;
        }

        /// <summary>
        /// Retreive all characters
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<CharacterDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Get([FromQuery] string? planetName)
        {
            if (string.IsNullOrWhiteSpace(planetName))
            {
                var (characters, isFromCache) = await mediator.Send(new GetCharactersQuery());
                Response.Headers.Append(HeaderConstants.XFromDatabase, isFromCache ? "No" : "Yes");

                foreach (var item in characters.Take(5))
                {
                    await characterService.AddCharacterData(item);
                }
                return Ok(characters);
            }
            else
            {
                var charactersByPlanet = await mediator.Send(new GetCharactersByPlanetQuery(planetName));
                return Ok(charactersByPlanet);
            }
        }

        /// <summary>
        /// Save character
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CharacterSaveDTO characterSaveDTO)
        {
            ValidationResult result = await validator.ValidateAsync(characterSaveDTO);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return BadRequest(result);
            }

            await mediator.Send(new SaveCharacterCommand(characterSaveDTO));
            return Created();
        }
    }
}