using System;
using System.Collections.Generic;
using BestMovies.Models;
using BestMovies.ViewModels;
using Xamarin.Forms;

namespace BestMovies.Views
{
    public partial class TVShowDetailsPage : ContentPage
    {
         TVShowDetailsViewModel ViewModel = new TVShowDetailsViewModel();

        public TVShowDetailsPage(TVShow myShow)
        {
            InitializeComponent();
            BindingContext = ViewModel;
            ViewModel.TVShow = myShow;
            ViewModel.LoadTVShowCommand.Execute(null);
        }
    }
}
