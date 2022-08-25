using AutoMapper;
using filmwebclone_API.Entities;
using filmwebclone_API.Helpers;
using filmwebclone_API.Models;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public MovieService(FilmwebCloneContext dbContext, IMapper mapper, IHttpContextAccessor httpContext, IFileStorageService fileStorageService, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContext = httpContext.HttpContext;
            _fileStorageService = fileStorageService;
            _userManager = userManager;

        }

        public async Task<LandingPageDto> GetAllMovies()
        {
            var top = 6;
            var today = DateTime.Today;

            var upcomingReleases = await _dbContext.Movies
                .Where(x => x.ReleaseDate > today)
                .OrderBy(x => x.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var inTheaters = await _dbContext.Movies
                .Where(x => x.InTheaters)
                .OrderBy(x => x.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var landingPageDto = new LandingPageDto();

            landingPageDto.Upcoming = _mapper.Map<List<MovieDto>>(upcomingReleases);
            landingPageDto.InTheaters = _mapper.Map<List<MovieDto>>(inTheaters);

            return landingPageDto;
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

            var averageVote = 0.0;
            var userVote = 0;

            if(await _dbContext.Ratings.AnyAsync(x=> x.MovieId == id))
            {
                averageVote = await _dbContext.Ratings.Where(x => x.MovieId == id).AverageAsync(x => x.Rate);

                if (_httpContext.User.Identity.IsAuthenticated)
                {
                    var email = _httpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                    var user = await _userManager.FindByEmailAsync(email);
                    var userId = user.Id;

                    var ratingDb = await _dbContext.Ratings.FirstOrDefaultAsync(x=>x.MovieId == id && x.UserId == userId);

                    if (ratingDb != null)
                    {
                        userVote = ratingDb.Rate; 
                    }
                }
            }

            var movieDto = _mapper.Map<MovieDto>(movie);
            movieDto.AverageVote = averageVote;
            movieDto.UserVote = userVote;
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

        public async Task<MoviePutGetDto> PutGet(int id)
        {
            var movieDto = await GetMovieById(id);
            if (movieDto == null)
            {
                return null;
            }

            var genresSelectedIds = movieDto.Genres.Select(x => x.Id).ToList();

            var allGenres = await GetAllGenres();
            var nonSelectedGenres = allGenres.Where(x => !genresSelectedIds.Contains(x.Id)).ToList();



            var movieTheatersIds = movieDto.MovieTheaters.Select(x => x.Id).ToList();

            var allMovieTheaters = await GetAllMovieTheaters();
            var nonSelectedMovieTheaters = allMovieTheaters.Where(x => !movieTheatersIds.Contains(x.Id)).ToList();

            var nonSelectedGenresDtos = _mapper.Map<List<GenreDto>>(nonSelectedGenres);
            var nonSelectedMovieTheatersDto = _mapper.Map<List<MovieTheaterDto>>(nonSelectedMovieTheaters);

            var response = new MoviePutGetDto();
            response.Movie = movieDto;
            response.SelectedGenres = movieDto.Genres;
            response.NonSelectedGenres = nonSelectedGenresDtos;
            response.SelectedMovieTheaters = movieDto.MovieTheaters;
            response.NonSelectedMovieTheaters = nonSelectedMovieTheatersDto;
            response.Actors = movieDto.Actors;

            return response;
        }

        public async Task<bool> Edit(int id, MovieCreateDto dto)
        {
            var movie = await _dbContext.Movies
                .Include(x => x.MoviesActors)
                .Include(x => x.MoviesGenres)
                .Include(x => x.MovieTheatersMovies)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie is null)
            {
                return false;
            }

            movie = _mapper.Map(dto, movie);

            if (dto.Poster != null)
            {
                movie.Poster = await _fileStorageService
                    .EditFile(containerName, 
                                dto.Poster, 
                                movie.Poster);
            }

            AnnotateActorsOrder(movie);

            await _dbContext.SaveChangesAsync();

            return true;



        }

        public async Task<bool> Delete(int id)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if(movie is null)
            {
                return false;
            }

            _dbContext.Movies.Remove(movie);

            await _dbContext.SaveChangesAsync();
            await _fileStorageService.DeleteFile(movie.Poster, containerName);

            return true;
        }

        public async Task<List<MovieDto>> FilterMovies(FilterMoviesDto filterMoviesDto)
        {
            var moviesQueryable = _dbContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filterMoviesDto.Title))
            {
                moviesQueryable = moviesQueryable.Where(x => x.Title.Contains(filterMoviesDto.Title));
            }

            if (filterMoviesDto.InTheaters)
            {
                moviesQueryable = moviesQueryable.Where(x => x.InTheaters);
            }

            if (filterMoviesDto.Upcoming)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(x => x.ReleaseDate > today);
            }

            if (filterMoviesDto.GenreId != 0)
            {
                moviesQueryable = moviesQueryable.Where(x => x.MoviesGenres.Select(y => y.GenreId)
                    .Contains(filterMoviesDto.GenreId));
            }

            await _httpContext.InsertParametersPaginationInHeader(moviesQueryable);
            var movies = await moviesQueryable
                                            .OrderBy(x => x.Title)
                                            .Paginate(filterMoviesDto.PaginationDto)
                                            .ToListAsync();

            return _mapper.Map<List<MovieDto>>(movies);
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
