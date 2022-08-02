using AutoMapper;
using filmwebclone_API.Entities;
using filmwebclone_API.Models;
using filmwebclone_API.Helpers;
using filmwebclone_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace filmwebclone_API.Services
{
    public class GenreService : IGenreService
    {
        private readonly FilmwebCloneContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        public GenreService(FilmwebCloneContext dbContext, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContext = httpContext.HttpContext;
        }

        public async Task<IEnumerable<GenreDto>> GetAll(PaginationDto paginationDto)
        {
            var queryable = _dbContext.Genres.AsQueryable();
            await _httpContext.InsertParametersPaginationInHeader(queryable);

            var genres = await queryable.OrderBy(x=>x.Name).Paginate(paginationDto).ToListAsync();
            var genresDto = _mapper.Map<List<GenreDto>>(genres);

            return genresDto;
        }

        public async Task<GenreDto> GetGenreById(int id)
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (genre is null)
            {
                return null;
            }
            var genreDto = _mapper.Map<GenreDto>(genre);

            return genreDto;
        }

        public async Task<int> Create(GenreCreateDto genreCreateDto)
        {
            var genre = _mapper.Map<Genre>(genreCreateDto);
            _dbContext.Genres.Add(genre);
            await _dbContext.SaveChangesAsync();

            return genre.Id;
        }

        public async Task<bool> Edit(int id, GenreCreateDto dto)
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (genre is null)
            {
                return false;
            }

            genre = _mapper.Map(dto, genre);
            await  _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
