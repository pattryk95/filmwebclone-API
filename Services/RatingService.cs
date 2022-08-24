using AutoMapper;
using filmwebclone_API.Entities;
using filmwebclone_API.Models;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services
{
    public class RatingService : IRatingService
    {
        private readonly FilmwebCloneContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly HttpContext _httpContext;

        public RatingService
            (
                FilmwebCloneContext dbContext,
                IMapper mapper,
                IHttpContextAccessor httpContext,
                UserManager<IdentityUser> userManager
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContext = httpContext.HttpContext;
            _userManager = userManager;

        }

        public async Task<bool> Add(RatingDto ratingDto)
        {
            var email = _httpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return false;
            }

            var userId = user.Id;

            var currentRate = await _dbContext.Ratings
                .FirstOrDefaultAsync(x => x.MovieId == ratingDto.MovieId && x.UserId == userId);
            if (currentRate == null)
            {
                var rating = new Rating();
                rating.MovieId = ratingDto.MovieId;
                rating.Rate = ratingDto.Rate;
                rating.UserId = userId;

                _dbContext.Add(rating);
            }
            else
            {
                currentRate.Rate = ratingDto.Rate;
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
