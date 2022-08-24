using filmwebclone_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Services.Interfaces
{
    public interface IRatingService
    {
        Task<bool> Add(RatingDto ratingDto);
    }
}
