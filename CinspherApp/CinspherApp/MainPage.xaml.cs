
using CinspherApp.Models;
using CinspherApp.Pages;
using CinspherApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace CinspherApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public ICommand RefreshCommand { get; }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);

        }
        public MainPage()
        {
            InitializeComponent();
            Tickets = GetTickets();
            GetMovies();
            this.BindingContext = this;
        }

        public ObservableCollection<Ticket> Tickets { get; set; }
        public ObservableCollection<Movie> Movies { get; set; }
        public Movie SelectedMovie { get; set; }

        private ObservableCollection<Ticket> GetTickets()
        {
            return new ObservableCollection<Ticket>
            {
                new Ticket { MovieTitle = "Bad Boys For LIfe", Image = "BadBoys.jpg", ShowingDate = DateTime.Now.AddDays(15), Seats = new int[] { 61, 62, 63 } },
                new Ticket { MovieTitle = "The Old Guard", Image = "OldGuard.jpg", ShowingDate = DateTime.Now.AddDays(22), Seats = new int[] {111, 112 } },
                new Ticket { MovieTitle = "Tenet", Image = "Tenet.jpg", ShowingDate = DateTime.Now.AddDays(25), Seats = new int[] { 12, 25, 35 } },
                new Ticket { MovieTitle = "No Time To Die", Image = "TimeToDie.jpg", ShowingDate = DateTime.Now.AddDays(20), Seats = new int[] { 99 } }
            };
        }
        private async void GetMovies()
        {
            Movies = new ObservableCollection<Movie>();
            var movies = await ApiServices.GetMovies();
            foreach (var r in movies)
            {



                Movies.Add(r);
            }
           
            //this.IsBusy = false;
        }
        private void TicketSelected(object sender, SelectionChangedEventArgs e)
        {
            //if (e.CurrentSelection != null)
            //{
            //    this.Navigation.PushAsync(new MovieDetailPage(SelectedMovie));
            //    //this.Navigation.PushAsync(new SeatsPage(SelectedTicket));

            //}

            var selectedMovie = e.CurrentSelection.FirstOrDefault() as Movie; // Assuming the selection is of type Movie
            if (selectedMovie != null)
            {
                this.Navigation.PushAsync(new MovieDetailPage(selectedMovie));
            }
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {

        }

        private void TapContact_Tapped(object sender, EventArgs e)
        {

        }

        private void TapSubscription_Tapped(object sender, EventArgs e)
        {

        }

        private async void TapLogout_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("accessToken", "");
            Preferences.Set("userId", "");
            Preferences.Set("userName", "");

            await Navigation.PushModalAsync(new Pages.Login());
        }

        private void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            CloseMenu();
        }
        private async void CloseMenu()
        {
            await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;

        }

        private void TapMyBusiness_Tapped(object sender, EventArgs e)
        {

        }

        private async void TapHome_Tapped(object sender, EventArgs e)
        {
            

        }

        private async void TapMyBooking_Tapped(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new Pages.MyBooking());
            await Navigation.PushAsync(new Pages.MyBooking());
        }
    }
    public class Ticket
    {
        public string MovieTitle { get; set; }
        public DateTime ShowingDate { get; set; }
        public string Image { get; set; }
        public int[] Seats { get; set; }
    }
}
