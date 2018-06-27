using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BestMovies.Models
{
    
    public class Genre 
    {
        public long Id { get; set; }
        public String Name { get; set; }
    }

    public class TVShow
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Genres { get; set; }
        public String ReleaseDate { get; set;  }
        public String Overview { get; set; }
        public String PosterThumb { get; set; }
        public String PosterImage { get; set; }
        public double Popularity { get; set; }
        public double VoteAverage { get; set; }
    }

}
