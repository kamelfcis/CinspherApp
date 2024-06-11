using System;
using System.Collections.Generic;
using System.Text;

namespace CinspherApp.Models
{
    public class Show
    {
        public int ShowId { get; set; }
        public string ShowDateTime { get; set; }
        public string ShowName { get; set; }

        public List<Seat> Seats { get; set; } // You might also need a ShowDTO
    }
}
