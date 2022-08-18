using AutoMapper;
using filmwebclone_API.Entities;
using filmwebclone_API.Helpers;
using filmwebclone_API.Models;
using filmwebclone_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services
{
    public class MovieService : IMovieService
    {
        private readonly FilmwebCloneContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "movies";

        public MovieService(FilmwebCloneContext dbContext, IMapper mapper, IHttpContextAccessor httpContext, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContext = httpContext.HttpContext;
            _fileStorageService = fileStorageService;

        }

        public async Task<MovieDto> GetMovieById(int id)
        {
            var movie = await _dbContext.Movies
                .Include(x => x.MoviesGenres).ThenInclude(x => x.Genre)
                .Include(x => x.MovieTheatersMovies).ThenInclude(x => x.MovieTheater)
                .Include(x => x.MoviesActors).ThenInclude(x => x.Actor)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                return null;
            }
            var movieDto = _mapper.Map<MovieDto>(movie);
            movieDto.Actors = movieDto.Actors.OrderBy(x=> x.Order).ToList();
            return movieDto;
        }

        public async Task<int> Create(MovieCreateDto movieCreateDto)
        {
            var movie = _mapper.Map<Movie>(movieCreateDto);
            if (movieCreateDto.Poster != null)
            {
                movie.Poster = await _fileStorageService.SaveFile(containerName, movieCreateDto.Poster);
            }

            AnnotateActorsOrder(movie);
            
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();

            return movie.Id;
        }
        public async Task<List<MovieTheaterDto>> GetAllMovieTheaters()
        {
            var movieTheaters = await _dbContext.MovieTheaters.ToListAsync();

            var movieTheatersDto = _mapper.Map<List<MovieTheaterDto>>(movieTheaters);

            return movieTheatersDto;
        }

        public async Task<List<GenreDto>> GetAllGenres()
        {
            var genres = await _dbContext.Genres.ToListAsync();

            var genresDto = _mapper.Map<List<GenreDto>>(genres);

            return genresDto;
        }

        private void AnnotateActorsOrder(Movie movie)
        {
            if(movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }
    }
}
