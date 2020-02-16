using System;
using System.Collections.Generic;
using System.Text;
using SharedTrip.Services.Models;

namespace SharedTrip.Services.Interfaces
{
    public interface ITripService
    {
        IEnumerable<TripListingServiceModel> GetAll();

        void Create(TripServiceModel tripModel);

        TripServiceModel GetById(string tripId);

        bool IsUserInTrip(string userId, string tripId);

        void AddUserToTrip(string userId, string tripId);
    }
}
