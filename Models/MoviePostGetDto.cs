﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Models
{
    public class MoviePostGetDto
    {
        public List<GenreDto> Genres { get; set; }
        public List<MovieTheaterDto> MovieTheaters { get; set; }
    }
}
