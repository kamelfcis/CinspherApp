using System;
using System.Collections.Generic;

namespace CinspherApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public string ShowName { get; set; }
        public DateTime BookingTime { get; set; }
        public List<Seat> Seats { get; set; }
        public string Note { get; set; }
    }
}
