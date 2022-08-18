using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Models
{
    public class ActorsMovieDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
    }
}
