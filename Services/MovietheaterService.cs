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
    public class MovietheaterService : IMovietheaterService
    {
        private readonly FilmwebCloneContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        public MovietheaterService(FilmwebCloneContext dbContext, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContext = httpContext.HttpContext;
        }

        public async Task<IEnumerable<MovieTheaterDto>> GetAll(PaginationDto paginationDto)
        {
            var queryable = _dbContext.MovieTheaters.AsQueryable();
            await _httpContext.InsertParametersPaginationInHeader(queryable);

            var movieTheaters = await queryable.OrderBy(x => x.Name).Paginate(paginationDto).ToListAsync();
            var movieTheatersDto = _mapper.Map<List<MovieTheaterDto>>(movieTheaters);

            return movieTheatersDto;
        }

        public async Task<MovieTheaterDto> GetMovieTheaterById(int id)
        {

            var movieTheater = await _dbContext.MovieTheaters.FirstOrDefaultAsync(mt => mt.Id == id);
            if (movieTheater is null)
            {
                return null;
            }
            var movieTheaterDto = _mapper.Map<MovieTheaterDto>(movieTheater);

            return movieTheaterDto;
        }

        public async Task<int> Create(MovieTheaterCreateDto movieTheaterCreateDto)
        {
            var movieTheater = _mapper.Map<MovieTheater>(movieTheaterCreateDto);
            _dbContext.MovieTheaters.Add(movieTheater);
            await _dbContext.SaveChangesAsync();

            return movieTheater.Id;
        }

        public async Task<bool> Edit(int id, MovieTheaterCreateDto dto)
        {
            var movieTheater = await _dbContext.MovieTheaters.FirstOrDefaultAsync(mt => mt.Id == id);
            if (movieTheater is null)
            {
                return false;
            }

            movieTheater = _mapper.Map(dto, movieTheater);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var movieTheaterExists = await _dbContext.MovieTheaters.AnyAsync(mt => mt.Id == id);
            if (!movieTheaterExists)
            {
                return false;
            }

            _dbContext.Remove(new MovieTheater()
            {
                Id = id,
            });
            await _dbContext.SaveChangesAsync();

            return true;

        }

    }
}
