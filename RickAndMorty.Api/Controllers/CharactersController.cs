﻿using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Application.Commands;
using RickAndMorty.Application.DTOs;
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

        public CharactersController(IMediator mediator, IValidator<CharacterSaveDTO> validator)
        {
            this.mediator = mediator;
            this.validator = validator;
        }

        // GET: api/<CharactersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var (characters, isFromCache) = await mediator.Send(new GetCharactersQuery());
            Response.Headers.Append("X-From-Database", isFromCache ? "No" : "Yes");
            return Ok(characters);
        }

        // POST api/<CharactersController>
        [HttpPost]
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