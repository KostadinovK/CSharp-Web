using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRunes.Data;
using IRunes.Models.Models;

namespace IRunes.Services
{
    public class TrackService : ITrackService
    {
        private readonly RunesDbContext context;

        public TrackService()
        {
            context = new RunesDbContext();
        }

        public Track GetTrackById(string trackId)
        {
            return context.Tracks.SingleOrDefault(t => t.Id == trackId);
        }
    }
}
