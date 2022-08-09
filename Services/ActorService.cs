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
    public class ActorService : IActorService
    {
        private readonly FilmwebCloneContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "actors";
        private readonly HttpContext _httpContext;

        public ActorService(FilmwebCloneContext dbContext, IMapper mapper, IFileStorageService fileStorageService, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _httpContext = httpContext.HttpContext;
        }

        public async Task<IEnumerable<ActorDto>> GetAll(PaginationDto paginationDto)
        {
            var queryable = _dbContext.Actors.AsQueryable();
            await _httpContext.InsertParametersPaginationInHeader(queryable);

            var actors = await queryable.OrderBy(a => a.LastName).Paginate(paginationDto).ToListAsync();
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
            if (actorCreateDto.Picture != null)
            {
                actor.Picture = await _fileStorageService.SaveFile(containerName, actorCreateDto.Picture);
            }
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

            if (dto.Picture != null)
            {
                actor.Picture = await _fileStorageService.EditFile(containerName,
                                                                    dto.Picture,
                                                                    actor.Picture);
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var actor = await _dbContext.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actor is null)
            {
                return false;
            }

            _dbContext.Actors.Remove(actor);
            await _dbContext.SaveChangesAsync();
            await _fileStorageService.DeleteFile(actor.Picture, containerName);

            return true;

        }
    }
}
