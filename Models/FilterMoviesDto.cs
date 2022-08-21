using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Models
{
    public class FilterMoviesDto
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public PaginationDto PaginationDto
        { 
            get { return new PaginationDto() { PageNumber = Page, RecordsPerPage = RecordsPerPage }; } 
        }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool InTheaters { get; set; }
        public bool Upcoming { get; set; }
    }
}
