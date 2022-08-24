using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Models
{
    public class RatingDto
    {
        [Range(1, 5)]
        public int Rate { get; set; }
        public int MovieId { get; set; }
    }
}
