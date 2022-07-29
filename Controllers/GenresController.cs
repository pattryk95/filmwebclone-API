using filmwebclone_API.Entities;
using filmwebclone_API.Models;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace filmwebclone_API.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
        {
            var genresDtos = await _genreService.GetAll();

            return Ok(genresDtos);
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
        public async Task<IActionResult> CreateGenre([FromBody] GenreCreateDto genreCreateDto)
        {
            var genreId = await _genreService.Create(genreCreateDto);

            return Created($"api/genres/{genreId}", null);
        }
    }
}
