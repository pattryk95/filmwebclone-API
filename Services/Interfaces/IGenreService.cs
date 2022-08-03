using filmwebclone_API.Entities;
using filmwebclone_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAll(PaginationDto paginationDto);
        Task<GenreDto> GetGenreById(int id);
        Task<int> Create(GenreCreateDto genreCreateDto);
        Task<bool> Edit(int id, GenreCreateDto dto);
        Task<bool> Delete(int id);
    }
}
