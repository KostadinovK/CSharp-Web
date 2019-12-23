using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Models.Models;

namespace IRunes.Services
{
    public interface ITrackService
    {
        Track GetTrackById(string trackId);
    }
}
