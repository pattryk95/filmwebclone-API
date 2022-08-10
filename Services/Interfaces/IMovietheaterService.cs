using filmwebclone_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services.Interfaces
{
    public interface IMovietheaterService
    {
        Task<IEnumerable<MovieTheaterDto>> GetAll(PaginationDto paginationDto);
        Task<MovieTheaterDto> GetMovieTheaterById(int id);
        Task<int> Create(MovieTheaterCreateDto movieTheaterCreateDto);
        Task<bool> Edit(int id, MovieTheaterCreateDto dto);
        Task<bool> Delete(int id);
    }
}
