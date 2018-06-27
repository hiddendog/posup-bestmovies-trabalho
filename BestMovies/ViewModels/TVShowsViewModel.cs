using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using BestMovies.Models;
using BestMovies.Views;
using BestMovies.Services;
using System.ComponentModel;
using System.Collections.Generic;

namespace BestMovies.ViewModels
{
    public class TVShowsViewModel : INotifyPropertyChanged
    {
        public Boolean IsBusy { get; private set; }
        public int CurrentPage { get; private set; }
        public ITVShowsStore TVShowsStore => DependencyService.Get<ITVShowsStore>();

        public PagedResult<TVShow> PopularTVShows { get; private set; }
        public Command LoadPopularTVShowsCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public TVShowsViewModel()
        {
            CurrentPage = 1;
            IsBusy = false;
            PopularTVShows = new PagedResult<TVShow>
            {
                Page = 0,
                TotalPages = 0,
                TotalResults = 0,
                Results = new List<TVShow>()
            };
            LoadPopularTVShowsCommand = new Command(async () => await ExecuteLoadPopularTVShowsCommand());
        }

        async Task ExecuteLoadPopularTVShowsCommand()
        {
            if (IsBusy) 
            {
                return;
            }
            IsBusy = true;
            try 
            { 
                PopularTVShows = TVShowsStore.ListPopular(CurrentPage);  
                OnPropertyChanged("PopularTVShows");
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