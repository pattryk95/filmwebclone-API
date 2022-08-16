using filmwebclone_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDto>> GetAll(PaginationDto paginationDto);
        Task<ActorDto> GetActorById(int id);
        Task<IEnumerable<ActorsMovieDto>> SearchByName(string query);
        Task<int> Create(ActorCreateDto actorCreateDto);
        Task<bool> Edit(int id, ActorCreateDto dto);
        Task<bool> Delete(int id);
    }
}
