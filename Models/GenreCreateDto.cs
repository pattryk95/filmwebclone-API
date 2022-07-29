﻿using filmwebclone_API.Entities.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Models
{
    public class GenreCreateDto
    {
        [Required(ErrorMessage ="The field with name {0} is required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; }
    }
}
