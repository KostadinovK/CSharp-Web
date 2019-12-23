using System;
using System.Collections.Generic;
using IRunes.Models.Models;

namespace IRunes.Services
{
    public interface IAlbumService
    {
        Album CreateAlbum(Album album);

        ICollection<Album> GetAllAlbums();

        Album GetAlbumById(string id);

        bool AddTrackToAlbum(string albumId, Track track);
    }
}
