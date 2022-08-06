using filmwebclone_API.Entities.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Models
{
    public class ActorCreateDto
    {
        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(120)]
        [FirstLetterUppercase]
        public string FirstName { get; set; }

        [StringLength(120)]
        [FirstLetterUppercase]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(120)]
        [FirstLetterUppercase]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? Biography { get; set; }
        public IFormFile? Picture { get; set; }
    }
}