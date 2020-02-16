using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services.Models
{
    public class TripListingServiceModel
    {
        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }
    }
}
