using System;
using System.Collections.Generic;
using System.Text;

namespace CinspherApp.Models
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int CinemaHallId { get; set; }

        public string CinemaHallName { get; set; }
        public bool IsBooked { get; set; }
    }
}
