using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedTrip.BindingModels.Trip;
using SharedTrip.Services.Interfaces;
using SharedTrip.Services.Models;
using SharedTrip.ViewModels.Trip;
using SIS.HTTP;
using SIS.MvcFramework;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private ITripService tripService;

        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
        }

        public HttpResponse All()
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Home/Index");
            }

            var trips = tripService
                .GetAll()
                .Where(t => t.Seats > 0)
                .Select(t => new TripListingViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats
                }).ToList();

            return this.View(trips.ToArray());
        }

        public HttpResponse Add()
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Home/Index");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddBindingModel input)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Home/Index");
            }

            if (string.IsNullOrEmpty(input.StartPoint) || string.IsNullOrWhiteSpace(input.StartPoint))
            {
                return Redirect("/Trips/Add");
            }

            if (string.IsNullOrEmpty(input.EndPoint) || string.IsNullOrWhiteSpace(input.EndPoint))
            {
                return Redirect("/Trips/Add");
            }

            if (string.IsNullOrEmpty(input.DepartureTime) || string.IsNullOrWhiteSpace(input.DepartureTime))
            {
                return Redirect("/Trips/Add");
            }

            if (string.IsNullOrEmpty(input.Description) || string.IsNullOrWhiteSpace(input.Description) || input.Description.Length > 80)
            {
                return Redirect("/Trips/Add");
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return Redirect("/Trips/Add");
            }

            var dateTimeParsed = new DateTime();

            if (!DateTime.TryParse(input.DepartureTime, out dateTimeParsed))
            {
                var dateArgs = input.DepartureTime.Split('.').ToList();
                var day = int.Parse(dateArgs[0]);
                var month = int.Parse(dateArgs[1]);

                var timeArgs = dateArgs[2].Split(' ').ToList();
                var year = int.Parse(timeArgs[0]);

                var hourArgs = timeArgs[1].Split(':').ToList();
                var hour = int.Parse(hourArgs[0]);
                var minutes = int.Parse(hourArgs[1]);

                var date = new DateTime(year, month, day, hour, minutes, 0);

                var trip = new TripServiceModel
                {
                    StartPoint = input.StartPoint,
                    EndPoint = input.EndPoint,
                    Seats = input.Seats,
                    Description = input.Description,
                    ImagePath = input.ImagePath,
                    DepartureTime = date
                };

                tripService.Create(trip);
            }
            else
            {
                var trip = new TripServiceModel
                {
                    StartPoint = input.StartPoint,
                    EndPoint = input.EndPoint,
                    Seats = input.Seats,
                    Description = input.Description,
                    ImagePath = input.ImagePath,
                    DepartureTime = dateTimeParsed
                };

                tripService.Create(trip);
            }

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Home/Index");
            }

            var tripServiceModel = tripService.GetById(tripId);

            var trip = new TripDetailsViewModel
            {
                Id = tripServiceModel.Id,
                Seats = tripServiceModel.Seats,
                StartPoint = tripServiceModel.StartPoint,
                EndPoint = tripServiceModel.EndPoint,
                Description = tripServiceModel.Description,
                ImagePath = tripServiceModel.ImagePath,
                DepartureTime = tripServiceModel.DepartureTime.ToString("dd.MM.yyyy HH:mm")
            };

            return this.View(trip);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Home/Index");
            }

            var userId = User;

            if (tripService.IsUserInTrip(userId, tripId))
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            tripService.AddUserToTrip(userId, tripId);

            return Redirect("/Home/Index");
        }
    }
}
