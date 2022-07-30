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

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize =  (value > maxPageSize) ? maxPageSize : value;
            }
        }


    }
}
