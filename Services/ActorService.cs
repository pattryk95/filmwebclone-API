using AutoMapper;
using filmwebclone_API.Entities;
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
    public class ActorService : IActorService
    {
        private readonly FilmwebCloneContext _dbContext;
        private readonly IMapper _mapper;

        public ActorService(FilmwebCloneContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActorDto>> GetAll()
        {
            var actors = await _dbContext.Actors.ToListAsync();
            var actorsDtos = _mapper.Map<List<ActorDto>>(actors);

            return actorsDtos;
        }

        public async Task<ActorDto> GetActorById(int id)
        {
            var actor = await _dbContext.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actor is null)
            {
                return null;
            }
            var actorDto = _mapper.Map<ActorDto>(actor);

            return actorDto;
        }
        public async Task<int> Create(ActorCreateDto actorCreateDto)
        {
            var actor = _mapper.Map<Actor>(actorCreateDto);
            _dbContext.Actors.Add(actor);
            await _dbContext.SaveChangesAsync();

            return actor.Id;
        }

        public async Task<bool> Edit(int id, ActorCreateDto dto)
        {
            var actor = await _dbContext.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actor is null)
            {
                return false;
            }

            actor = _mapper.Map(dto, actor);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var actorExists = await _dbContext.Actors.AnyAsync(a => a.Id == id);
            if (!actorExists)
            {
                return false;
            }

            _dbContext.Remove(new Actor()
            {
                Id = id,
            });
            await _dbContext.SaveChangesAsync();

            return true;

        }
    }
}
