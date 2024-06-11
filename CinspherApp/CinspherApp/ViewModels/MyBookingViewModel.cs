using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using CinspherApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CinspherApp.ViewModels
{
    public class MyBookingViewModel : BindableObject
    {
        private ObservableCollection<Booking> bookings;
        public ICommand CancelCommand { get; }
        public ObservableCollection<Booking> Bookings
        {
            get => bookings;
            set
            {
                bookings = value;
                OnPropertyChanged();
            }
        }

        public MyBookingViewModel()
        {
            LoadBookings();
            CancelCommand = new Command<Booking>(OnCancelBooking);
        }

        private void OnCancelBooking(Booking booking)
        {
            // Remove the booking from the collection
            if (booking != null && Bookings.Contains(booking))
            {
                Bookings.Remove(booking);
            }
        }

        private async void LoadBookings()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://cinespherapp2024.somee.com/api/Booking/GetBookingByUserId/1");
            var bookingList = JsonConvert.DeserializeObject<List<Booking>>(response);
            Bookings = new ObservableCollection<Booking>(bookingList);
        }
    }

    
}
