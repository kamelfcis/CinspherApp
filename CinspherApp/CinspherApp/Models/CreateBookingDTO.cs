using System;
using System.Collections.Generic;
using System.Text;

namespace CinspherApp.Models
{
    public class CreateBookingDTO
    {
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public List<int> SeatIds { get; set; }
        public string Note { get; set; }
    }
}
