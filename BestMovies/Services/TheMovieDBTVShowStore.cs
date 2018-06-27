using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestMovies.Models;
using Newtonsoft.Json;
using RestSharp.Serializers.Newtonsoft.Json;
using Xamarin.Forms;
using System.Globalization;

[assembly: Dependency(typeof(BestMovies.Services.TheMovieDB.TheMovieDBTVShowStore))]
namespace BestMovies.Services.TheMovieDB
{
    public class TheMovieDBTVShowStore : ITVShowsStore
    {

        private IDictionary<long, Genre> _GenreDict = null;
        private IDictionary<long, Genre> GenreDict
        {
            get 
            {
                if (_GenreDict == null) 
                {
                    _GenreDict = ListGenres();
                }
                return _GenreDict;
            }
        }

        [JsonObject]
        private class GenreResponse 
        {
            [JsonProperty("genres")]
            public List<Genre> Genres { get; set; }
        }

        [JsonObject]
        public class TVShowDetailsResponse
        {
            [JsonProperty("first_air_date")]
            public String FirstAirDate { get; set; }
        }

        [JsonObject]
        public class TVShowResponse
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("original_name")]
            public String OriginalName { get; set; }
            [JsonProperty("genre_ids")]
            public List<long> GenreIds { get; set; }
            [JsonProperty("name")]
            public String Name { get; set; }
            [JsonProperty("popularity")]
            public double Popularity { get; set; }
            [JsonProperty("country")]
            public List<String> CountryCodes { get; set; }
            [JsonProperty("original_language")]
            public String OriginalLanguage { get; set; }
            [JsonProperty("vote_average")]
            public double VoteAverage { get; set; }
            [JsonProperty("overview")]
            public String Overview { get; set; }
            [JsonProperty("poster_path")]
            public String PosterPath { get; set; }
        }

        public TVShow GetTVShowDetails(int id)
        {
            var client = new RestSharp.RestClient("https://api.themoviedb.org/3");

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestSharp.RestRequest("tv/"+id);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            request.AddHeader("Accept", "application/json");
            request.AddParameter("api_key", "ad332258020257fb88e2cc468225dcb0");
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            var response = client.ExecuteAsGet<TVShowDetailsResponse>(request, "GET");
            return new TVShow
            {
                ReleaseDate = DateTime.ParseExact(response.Data.FirstAirDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy")
            };
        }

        private IDictionary<long, Genre> ListGenres() 
        {
            var client = new RestSharp.RestClient("https://api.themoviedb.org/3");

            IDictionary<long, Genre> dict = new Dictionary<long, Genre>();
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestSharp.RestRequest("genre/tv/list");
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            request.AddHeader("Accept", "application/json");
            request.AddParameter("api_key", "ad332258020257fb88e2cc468225dcb0");
            request.JsonSerializer = new NewtonsoftJsonSerializer();

            var response = client.ExecuteAsGet<GenreResponse>(request, "GET");
            response.Data.Genres.ForEach((Genre obj) => dict.Add(obj.Id, obj));
            request = new RestSharp.RestRequest("genre/movie/list");
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            request.AddHeader("Accept", "application/json");
            request.AddParameter("api_key", "ad332258020257fb88e2cc468225dcb0");
            request.JsonSerializer = new NewtonsoftJsonSerializer();

            response = client.ExecuteAsGet<GenreResponse>(request, "GET");
            response.Data.Genres.ForEach((Genre obj) => {
                if (!dict.ContainsKey(obj.Id)) {
                    dict.Add(obj.Id, obj);
                }
            });
            return dict;
        }

      
        PagedResult<TVShow> ITVShowsStore.ListPopular(int page)
        {
            var client = new RestSharp.RestClient("https://api.themoviedb.org/3");

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestSharp.RestRequest("tv/popular");
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            request.AddHeader("Accept", "application/json");
            request.AddParameter("api_key", "ad332258020257fb88e2cc468225dcb0");
            request.AddParameter("language", "en");
            request.AddParameter("page", page);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            var response = client.ExecuteAsGet<PagedResult<TVShowResponse>>(request, "GET");
            return new PagedResult<TVShow>
            {
                Results = from showResponse in response.Data.Results
                                                       select new TVShow
                {
                   Id = showResponse.Id,
                   Genres = String.Join(", ", from genreId in showResponse.GenreIds select GetGenreById(genreId).Name),
                   Name = showResponse.Name,
                   Overview = showResponse.Overview,
                   PosterThumb = "https://image.tmdb.org/t/p/w92" + showResponse.PosterPath,
                   PosterImage = "https://image.tmdb.org/t/p/w300" + showResponse.PosterPath,
                   Popularity = showResponse.Popularity,
                   VoteAverage = showResponse.VoteAverage
                },                              
                TotalPages = response.Data.TotalPages,
                Page = response.Data.Page,
                TotalResults = response.Data.TotalResults
            };
        }

        public Genre GetGenreById(long id)
        {
            return GenreDict[id];
        }
    }
}
