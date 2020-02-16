using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedTrip.Models;
using SharedTrip.Services.Interfaces;
using SharedTrip.Services.Models;

namespace SharedTrip.Services.Implementations
{
    public class TripService : ITripService
    {
        private ApplicationDbContext context;

        public TripService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TripListingServiceModel> GetAll()
        {
            return context.Trips
                .Select(t => new TripListingServiceModel
                {
                    Id =  t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime,
                    Seats = t.Seats
                })
                .ToList();
        }

        public void Create(TripServiceModel tripModel)
        {
            var trip = new Trip()
            {
                StartPoint = tripModel.StartPoint,
                EndPoint = tripModel.EndPoint,
                Description = tripModel.Description,
                Seats = tripModel.Seats,
                ImagePath = tripModel.ImagePath,
                DepartureTime = tripModel.DepartureTime
            };

            context.Trips.Add(trip);
            context.SaveChanges();
        }

        public TripServiceModel GetById(string tripId)
        {
            var tripFromDb = context.Trips.SingleOrDefault(t => t.Id == tripId);

            if (tripFromDb == null)
            {
                return null;
            }

            var trip = new TripServiceModel()
            {
                Id = tripFromDb.Id,
                StartPoint = tripFromDb.StartPoint,
                EndPoint = tripFromDb.EndPoint,
                Description = tripFromDb.Description,
                DepartureTime = tripFromDb.DepartureTime,
                ImagePath = tripFromDb.ImagePath,
                Seats = tripFromDb.Seats
            };

            return trip;
        }

        public bool IsUserInTrip(string userId, string tripId)
        {
            if (context.UserTrip.Any(ut => ut.UserId == userId && ut.TripId == tripId))
            {
                return true;
            }

            return false;
        }

        public void AddUserToTrip(string userId, string tripId)
        {
            var userTrip = new UserTrip
            {
                UserId = userId,
                TripId = tripId
            };

            var trip = context.Trips.SingleOrDefault(t => t.Id == tripId);
            var user = context.Users.SingleOrDefault(u => u.Id == userId);

            trip.UserTrips.Add(userTrip);
            user.UserTrips.Add(userTrip);

            trip.Seats--;

            context.Trips.Update(trip);
            context.SaveChanges();
        }
    }
}
