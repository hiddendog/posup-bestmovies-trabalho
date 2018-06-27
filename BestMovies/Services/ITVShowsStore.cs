using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestMovies.Models;

namespace BestMovies.Services
{
    public interface ITVShowsStore
    {
        PagedResult<TVShow> ListPopular(int page);
        Genre GetGenreById(long id);
        TVShow GetTVShowDetails(int id);
    }
}
