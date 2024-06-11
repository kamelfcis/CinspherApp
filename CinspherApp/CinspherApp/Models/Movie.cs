using System;
using System.Collections.Generic;
using System.Text;

namespace CinspherApp.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int? Duration { get; set; }
        public float? Rating { get; set; }
        public string MoviePictureUrl { get; set; }
        public List<Show> Shows { get; set; } // You might also need a ShowDTO
    }
}
