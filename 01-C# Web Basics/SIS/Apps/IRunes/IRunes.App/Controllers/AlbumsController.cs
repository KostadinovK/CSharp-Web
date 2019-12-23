using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using IRunes.App.ViewModels;
using IRunes.Data;
using IRunes.Models.Models;
using IRunes.Services;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Requests;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes.Http;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Mapping;
using SIS.MvcFramework.Result;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;
        public AlbumsController()
        {
            albumService = new AlbumService();
        }

        [Authorize]
        public ActionResult All()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<div>");

            var albums = albumService.GetAllAlbums();

            if (albums.Count == 0)
            {
                sb.AppendLine("<p>No albums yet</p>");
            }
            else
            {
                foreach (var album in albums.ToList())
                {
                    sb.AppendLine($"<a class=\"text-primary font-weight-bold\" href=/Albums/Details?id={album.Id}>{WebUtility.UrlDecode(album.Name)}</a>");
                }
            }


            sb.AppendLine("</div>");

            ViewData.Add("Albums", sb.ToString().TrimEnd());

            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm()
        {
            var name = ((ISet<string>) Request.FormData["name"]).FirstOrDefault();
            var cover = ((ISet<string>) Request.FormData["cover"]).FirstOrDefault();

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

            albumService.CreateAlbum(album);

            return Redirect("/Albums/All");
        }

        [Authorize]
        public ActionResult Details()
        {
            var albumId = Request.QueryData["id"].ToString();

            var album = albumService.GetAlbumById(albumId);

            var albumViewModel = ModelMapper.ProjectTo<AlbumViewModel>(album);

            if (album == null)
            {
                return Redirect("/Albums/All");
            }

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
