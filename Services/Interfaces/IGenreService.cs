using filmwebclone_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetGenreById(int id);
        Task<int> Create(Genre genre);
    }
}
