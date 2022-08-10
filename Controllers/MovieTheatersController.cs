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
    [Route("api/movietheaters")]
    [ApiController] // for model validation
    public class MovieTheatersController : ControllerBase
    {
        private readonly IMovietheaterService _movietheaterService;
        public MovieTheatersController(IMovietheaterService movietheaterService)
        {
            _movietheaterService = movietheaterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieTheaterDto>>> GetAll([FromQuery] PaginationDto paginationDto)
        {
            var movieTheatersDtos = await _movietheaterService.GetAll(paginationDto);

            return Ok(movieTheatersDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieTheaterDto>> Get([FromRoute] int id)
        {
            var movieTheaterDto = await _movietheaterService.GetMovieTheaterById(id);
            if (movieTheaterDto is null)
            {
                return NotFound();
            }

            return Ok(movieTheaterDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovieTheater([FromBody] MovieTheaterCreateDto movieTheaterCreateDto)
        {
            var movieTheaterId = await _movietheaterService.Create(movieTheaterCreateDto);

            return Created($"api/movietheaters/{movieTheaterId}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMovieTheater([FromRoute] int id, [FromBody] MovieTheaterCreateDto dto)
        {
            var isUpdated = await _movietheaterService.Edit(id, dto);

            if (isUpdated == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieTheater([FromRoute] int id)
        {
            var isDeleted = await _movietheaterService.Delete(id);

            if (isDeleted == false)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
