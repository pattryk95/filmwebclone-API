using filmwebclone_API.Entities;
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
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAll();

            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var genre = await _genreService.GetGenreById(id);

            if (genre is null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] Genre genre)
        {
            var genreId = await _genreService.Create(genre);

            return Created($"api/genres/{genre.Id}", null);
        }
    }
}
