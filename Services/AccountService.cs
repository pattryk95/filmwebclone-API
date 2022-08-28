using AutoMapper;
using filmwebclone_API.Entities;
using filmwebclone_API.Helpers;
using filmwebclone_API.Models;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly FilmwebCloneContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        public AccountService(FilmwebCloneContext dbContext, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContext = httpContext.HttpContext;

        }

        public async Task<IEnumerable<UserDto>> GetUsers(PaginationDto paginationDto)
        {
            var queryable = _dbContext.Users.AsQueryable();
            await _httpContext.InsertParametersPaginationInHeader(queryable);

            var users = await queryable.OrderBy(x => x.Email).Paginate(paginationDto).ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
