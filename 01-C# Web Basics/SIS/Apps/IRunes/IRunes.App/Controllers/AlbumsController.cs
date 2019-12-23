using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using IRunes.Data;
using IRunes.Models.Models;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        public IHttpResponse All(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/");
            }

            var sb = new StringBuilder();

            sb.AppendLine("<div>");

            using (var context = new RunesDbContext())
            {
                if (!context.Albums.Any())
                {
                    sb.AppendLine("<p>No albums yet</p>");
                }
                else
                {
                    foreach (var album in context.Albums.ToList())
                    {
                        sb.AppendLine($"<a class=\"text-primary font-weight-bold\" href=/Albums/Details?id={album.Id}>{WebUtility.UrlDecode(album.Name)}</a>");
                    }
                }
            }

            sb.AppendLine("</div>");

            ViewData.Add("Albums", sb.ToString().TrimEnd());

            return View();
        }

        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost(ActionName = "Create")]
        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/");
            }

            var name = ((ISet<string>) httpRequest.FormData["name"]).FirstOrDefault();
            var cover = ((ISet<string>) httpRequest.FormData["cover"]).FirstOrDefault();

            using (var context = new RunesDbContext())
            {
                var album = new Album
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Cover = cover,
                    Price = 0
                };

                if (!IsValid(album))
                {
                    return Redirect("/Albums/Create");
                }

                context.Albums.Add(album);
                context.SaveChanges();
            }

            return Redirect("/Albums/All");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/");
            }

            var albumId = httpRequest.QueryData["id"].ToString();

            using var context = new RunesDbContext();

            var album = context.Albums
                .Where(a => a.Id == albumId)
                .Include(a => a.Tracks)
                .FirstOrDefault();

            ViewData.Add("AlbumId", album.Id);
            ViewData.Add("AlbumName", WebUtility.UrlDecode(album.Name));
            ViewData.Add("AlbumCover", WebUtility.UrlDecode(album.Cover));
            ViewData.Add("AlbumPrice", $"${album.Price:f2}");
            ViewData.Add("AlbumTracks", GetAlbumTracksHtml(album));

            return View();
        }

        private string GetAlbumTracksHtml(Album album)
        {
            if (!album.Tracks.Any())
            {
                return "<p>No tracks yet</p>";
            }

            var sb = new StringBuilder();

            var tracks = album.Tracks
                .Select(t => new
                {
                    Name = t.Name,
                    Id = t.Id,
                    AlbumId = t.AlbumId
                })
                .ToList();

            for (int i = 0; i < tracks.Count; i++)
            {
                sb.AppendLine($"<li><strong>{i + 1}. </strong><a href=\"/Tracks/Details?albumId={tracks[i].AlbumId}&trackId={tracks[i].Id}\">{WebUtility.UrlDecode(tracks[i].Name)}</a></li>");
            }

            return sb.ToString().TrimEnd();
        }

        protected bool IsValid(object obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);


            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
