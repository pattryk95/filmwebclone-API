using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Models
{
    public class PaginationDto
    {
        public int PageNumber { get; set; } = 1;
        
        private readonly int maxPageSize = 50;

        private int _recordsPerPage = 10;
        public int RecordsPerPage
        {
            get
            {
                return _recordsPerPage;
            }
            set
            {
                _recordsPerPage =  (value > maxPageSize) ? maxPageSize : value;
            }
        }


    }
}
