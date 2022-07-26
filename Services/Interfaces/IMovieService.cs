﻿using filmwebclone_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services.Interfaces
{
    public interface IMovieService
    {
        Task<int> Create(MovieCreateDto movieCreateDto);
        Task<List<MovieTheaterDto>> GetAllMovieTheaters();
        Task<List<GenreDto>> GetAllGenres();
        Task<MovieDto> GetMovieById(int id);
        Task<LandingPageDto> GetAllMovies();
        Task<MoviePutGetDto> PutGet(int id);
        Task<bool> Edit(int id, MovieCreateDto dto);
        Task<bool> Delete(int id);
        Task<List<MovieDto>> FilterMovies(FilterMoviesDto filterMoviesDto);

    }
}
