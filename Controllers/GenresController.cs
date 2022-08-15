﻿using filmwebclone_API.Models;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace filmwebclone_API.Controllers
{
    [Route("api/genres")]
    [ApiController] // for model validation
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll([FromQuery] PaginationDto paginationDto)
        {
            var genresDto = await _genreService.GetAll(paginationDto);

            return Ok(genresDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> Get([FromRoute] int id)
        {
            var genreDto = await _genreService.GetGenreById(id);

            if (genreDto is null)
            {
                return NotFound();
            }

            return Ok(genreDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateGenre([FromBody] GenreCreateDto genreCreateDto)
        {
            var genreId = await _genreService.Create(genreCreateDto);

            return Created($"api/genres/{genreId}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditGenre([FromRoute] int id, [FromBody] GenreCreateDto dto)
        {
            var isUpdated = await _genreService.Edit(id, dto);

            if (isUpdated == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id)
        {
            var isDeleted = await _genreService.Delete(id);

            if (isDeleted == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
