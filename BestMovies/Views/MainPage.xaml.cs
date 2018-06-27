using System;
using BestMovies.Models;
using RestSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BestMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
           
        }
    }
}