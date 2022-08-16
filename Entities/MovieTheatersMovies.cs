using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Entities
{
    public class MovieTheatersMovies
    {
        public int MovieTheaterId { get; set; }
        public MovieTheater MovieTheater { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
