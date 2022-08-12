using filmwebclone_API.Entities.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        [FirstLetterUppercase]
        public string FirstName { get; set; }

        [FirstLetterUppercase]
        public string? MiddleName { get; set; }

        [FirstLetterUppercase]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? Biography { get; set; }
        public string? Picture { get; set; }

        public List<Movie> Movies { get; set; }

    }
}
