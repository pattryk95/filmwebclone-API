using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Models
{
    public class LandingPageDto
    {
        public List<MovieDto> InTheaters { get; set; }
        public List<MovieDto> Upcoming { get; set; }
    }
}
