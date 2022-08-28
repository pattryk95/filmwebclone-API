using filmwebclone_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<UserDto>> GetUsers(PaginationDto paginationDto);
    }
}
