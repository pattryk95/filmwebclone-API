using filmwebclone_API.Helpers;
using filmwebclone_API.Models;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetAll([FromQuery] PaginationDto paginationDto)
        {
            var actorDtos = await _actorService.GetAll(paginationDto);

            return Ok(actorDtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> Get([FromRoute] int id)
        {
            var actorDto = await _actorService.GetActorById(id);

            if (actorDto is null)
            {
                return NotFound();
            }

            return Ok(actorDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateActor([FromForm] ActorCreateDto actorCreateDto)
        {
            var actorId = await _actorService.Create(actorCreateDto);

            return Created($"api/genres/{actorId}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActor([FromRoute] int id, [FromForm] ActorCreateDto dto)
        {
            var isUpdated = await _actorService.Edit(id, dto);

            if (isUpdated == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor([FromRoute] int id)
        {
            var isDeleted = await _actorService.Delete(id);

            if (isDeleted == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
