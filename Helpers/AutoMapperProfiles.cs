using AutoMapper;
using filmwebclone_API.Entities;
using filmwebclone_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDto>().ReverseMap();
            //                .ForMember(m => m.Name, c => c.MapFrom(s => s.Name));
            CreateMap<GenreCreateDto, Genre>();
        }
    }
}
