using AutoMapper;
using filmwebclone_API.Entities;
using filmwebclone_API.Models;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genre, GenreDto>().ReverseMap();
            //                .ForMember(m => m.Name, c => c.MapFrom(s => s.Name));
            CreateMap<GenreCreateDto, Genre>();

            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<ActorCreateDto, Actor>()
                .ForMember(x => x.Picture, options => options.Ignore());

            CreateMap<MovieTheater, MovieTheaterDto>()
                .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
                .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<MovieTheaterCreateDto, MovieTheater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto => 
                geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

            CreateMap<MovieCreateDto, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(x => x.MovieTheatersMovies, options => options.MapFrom(MapMovieTheatersMovies))
                .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActors));

        }

        private List<MoviesGenres> MapMoviesGenres(MovieCreateDto movieCreateDto, Movie movie)
        {
            var result = new List<MoviesGenres>();

            if (movieCreateDto.GenresId == null)
            {
                return result;
            }

            foreach (var id in movieCreateDto.GenresId)
            {
                result.Add(new MoviesGenres()
                {
                    GenreId = id,
                });

            }
                return result; 

        }

        private List<MovieTheatersMovies> MapMovieTheatersMovies(MovieCreateDto movieCreateDto, Movie movie)
        {
            var result = new List<MovieTheatersMovies>();

            if (movieCreateDto.MovieTheatersId == null)
            {
                return result;
            }

            foreach (var id in movieCreateDto.MovieTheatersId)
            {
                result.Add(new MovieTheatersMovies()
                {
                    MovieTheaterId = id,
                });

            }
                return result;

        }

        private List<MoviesActors> MapMoviesActors(MovieCreateDto movieCreateDto, Movie movie)
        {
            var result = new List<MoviesActors>();

            if (movieCreateDto.Actors == null)
            {
                return result;
            }

            foreach (var actor in movieCreateDto.Actors)
            {
                result.Add(new MoviesActors()
                {
                    ActorId = actor.Id,
                    Character = actor.Character
                });

            }
                return result;
        }
    }
}
