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
        [Required]
        [StringLength(120)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(120)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(120)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public IFormFile Picture { get; set; }
    }
}
