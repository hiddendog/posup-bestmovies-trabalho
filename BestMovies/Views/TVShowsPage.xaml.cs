using System;
using System.Collections.Generic;
using BestMovies.Models;
using BestMovies.ViewModels;
using Xamarin.Forms;

namespace BestMovies.Views
{
    public partial class TVShowsPage : ContentPage
    {
        TVShowsViewModel ViewModel = new TVShowsViewModel();

        public TVShowsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            ViewModel.LoadPopularTVShowsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args) 
        {
            var item = args.SelectedItem as TVShow;
            if (item == null)
                return;
            await Navigation.PushAsync(new TVShowDetailsPage(item));
        }
    }
}
