using AutoMapper;
using filmwebclone_API.Entities;
using filmwebclone_API.Models;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace filmwebclone_API.Services
{
    public class GenreService : IGenreService
    {
        private readonly FilmwebCloneContext _dbContext;
        private readonly IMapper _mapper;
        public GenreService(FilmwebCloneContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> GetAll()
        {
            var genres = await _dbContext.Genres.ToListAsync();
            var genresDto = _mapper.Map<List<GenreDto>>(genres);

            return genresDto;
        }

        public async Task<GenreDto> GetGenreById(int id)
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
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

    }
}
