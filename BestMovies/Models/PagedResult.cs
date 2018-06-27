using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace BestMovies.Models
{
    [JsonObject]
    public class PagedResult<T>
    {
        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}
