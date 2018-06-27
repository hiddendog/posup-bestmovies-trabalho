using System;
using System.ComponentModel;
using System.Threading.Tasks;
using BestMovies.Models;
using BestMovies.Services;
using Xamarin.Forms;

namespace BestMovies.ViewModels
{
    public class TVShowDetailsViewModel : INotifyPropertyChanged
    {
        public Boolean IsBusy { get; private set; }
        public ITVShowsStore TVShowsStore => DependencyService.Get<ITVShowsStore>();

        public TVShow TVShow { get; set; }
        public Command LoadTVShowCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public TVShowDetailsViewModel()
        {
            IsBusy = false;
            TVShow = new TVShow
            {
                Name = "Not loaded"
            };
            LoadTVShowCommand = new Command(async () => await ExecuteLoadTVShowCommand());
        }

        async Task ExecuteLoadTVShowCommand()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                TVShow newShow = TVShowsStore.GetTVShowDetails(TVShow.Id);
                this.TVShow = new TVShow
                {
                    Genres = this.TVShow.Genres,
                    Id = this.TVShow.Id,
                    Name = this.TVShow.Name,
                    VoteAverage = this.TVShow.VoteAverage,
                    ReleaseDate = newShow.ReleaseDate,
                    Overview = this.TVShow.Overview,
                    Popularity = this.TVShow.Popularity,
                    PosterThumb = this.TVShow.PosterThumb,
                    PosterImage = this.TVShow.PosterImage
                };
                OnPropertyChanged("TVShow");
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
