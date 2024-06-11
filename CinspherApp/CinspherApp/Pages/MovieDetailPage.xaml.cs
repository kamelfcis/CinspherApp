
using CinspherApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinspherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailPage : ContentPage
    {

        public MovieDetailPage(Movie selectedMovie)
        {
            InitializeComponent();
            DisplayMovieData(selectedMovie);
            CreateShowButtons(selectedMovie);
        }
        private async void AnimateButton(Button button)
        {
            while (true)
            {
                await button.ScaleTo(1.1, 1000, Easing.SpringIn);
                await button.ScaleTo(1.0, 1000, Easing.SpringOut);
            }
        }
        private async void OnShowButtonClicked(Show show,string image,string t)
        {
            await this.Navigation.PushAsync(new SeatsPage(show,image,t));
            // Handle the button click event, possibly navigate to another page with show details
             //DisplayAlert("Show Selected", $"You selected {show.ShowName}", "OK");
        }
        private void CreateShowButtons(Movie m)
        {
            if (m.Shows == null || m.Shows.Count == 0) return;

            foreach (var show in m.Shows)
            {
                var showButton = new Button
                {
                    Text = $"{show.ShowName}",
                    Style = (Style)Resources["ButtonStyle"],
                    HeightRequest = 40,
                    WidthRequest = 15,
                };
                showButton.Clicked += (s, e) => OnShowButtonClicked(show,m.MoviePictureUrl,m.Title);
                ShowsStackLayout.Children.Add(showButton);
            }
        }
        private void DisplayMovieData(Movie movie)
        {
            MovieImage.Source = movie.MoviePictureUrl;
            MovieTitle.Text = movie.Title;
            MovieDescription.Text = movie.Description;
            MovieType.Text = movie.Genre;
            MovieDuration.Text = $"{movie.Duration}h";
            MovieRating.Text = movie.Rating.ToString();
        }
    }
}