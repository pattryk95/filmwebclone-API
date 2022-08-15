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
                .ForMember(x => x.Poster, options => options.Ignore());

        }
    }
}
