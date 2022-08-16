using filmwebclone_API.Models;
using filmwebclone_API.Services;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService, IMovietheaterService movieTheatersService)
        {
            _movieService = movieService;
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<MoviePostGetDto>> PostGet()
        {
            var movieTheatersDto = await _movieService.GetAllMovieTheaters();
            var genresDto = await _movieService.GetAllGenres();

            return new MoviePostGetDto()
            {
                Genres = genresDto,
                MovieTheaters = movieTheatersDto
            };
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie([FromForm] MovieCreateDto movieCreateDto)
        {
            var movieId = await _movieService.Create(movieCreateDto);

            return Created($"api/movies/{movieId}", movieId);
        }
    }
}
