using MediatR;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Application.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RickAndMorty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IMediator mediator;
        public CharactersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<CharactersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var characters=await mediator.Send(new GetCharactersQuery());
           return Ok(characters);
        }

        // GET api/<CharactersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CharactersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CharactersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CharactersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
