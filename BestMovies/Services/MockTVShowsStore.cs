using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BestMovies.Models;

[assembly: Xamarin.Forms.Dependency(typeof(BestMovies.Services.Mock.MockTVShowsStore))]
namespace BestMovies.Services.Mock
{
    public class MockTVShowsStore : ITVShowsStore
    {
        PagedResult<TVShow> items;

        public MockTVShowsStore()
        {
            items = new PagedResult<TVShow>
            {
                TotalPages = 0,
                TotalResults = 0,
                Page = 0,
                Results = new List<TVShow>()
            };
        }

        public Task<PagedResult<TVShow>> ListPopularAsync(int page)
        {
            return Task.FromResult<PagedResult<TVShow>>(items);
        }
    }
}