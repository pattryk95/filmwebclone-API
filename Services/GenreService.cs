using filmwebclone_API.Entities;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace filmwebclone_API.Services
{
    public class GenreService : IGenreService
    {
        private readonly FilmwebCloneContext _dbContext;
        public GenreService(FilmwebCloneContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = _dbContext.Genres.ToListAsync();
            return await genres;
        }

        public async Task<Genre> GetGenreById(int id)
        {
            var genre = _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
            return await genre;
        }

        public async Task<int> Create(Genre genre)
        {
            _dbContext.Genres.Add(genre);
            await _dbContext.SaveChangesAsync();

            return genre.Id;
        }

    }
}
