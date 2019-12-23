using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRunes.Data;
using IRunes.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunes.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly RunesDbContext context;

        public AlbumService()
        {
            context = new RunesDbContext();
        }

        public Album CreateAlbum(Album album)
        {
            album = context.Albums.Add(album).Entity;
            context.SaveChanges();

            return album;
        }

        public ICollection<Album> GetAllAlbums()
        {
            return context.Albums.ToList();
        }

        public Album GetAlbumById(string id)
        {
            return context.Albums
                .Include(a => a.Tracks)
                .SingleOrDefault(a => a.Id == id);
        }

        public bool AddTrackToAlbum(string albumId, Track track)
        {
            var album = GetAlbumById(albumId);

            if (album == null)
            {
                return false;
            }

            album.Tracks.Add(track);
            album.Price = album.Tracks.Sum(t => t.Price) * 0.87m;

            context.Update(album);
            context.SaveChanges();

            return true;
        }
    }
}
