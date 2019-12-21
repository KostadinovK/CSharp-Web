using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using IRunes.Data;
using IRunes.Models.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/");
            }

            var albumId = httpRequest.QueryData["albumId"];
            ViewData.Add("AlbumId", albumId);

            return View();
        }

        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/");
            }

            var albumId = httpRequest.QueryData["albumId"].ToString();
            var name = ((ISet<string>) httpRequest.FormData["name"]).FirstOrDefault();
            var link = ((ISet<string>)httpRequest.FormData["link"]).FirstOrDefault();
            var price = ((ISet<string>)httpRequest.FormData["price"]).FirstOrDefault();

            using var context = new RunesDbContext();

            var track = new Track
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Link = link,
                Price = decimal.Parse(price),
                AlbumId = albumId
            };

            if (!IsValid(track))
            {
                return Redirect($"Albums/Details?id={albumId}");
            }

            context.Tracks.Add(track);

            var album = context.Albums.Find(albumId);
            album.Price = album.Tracks.Sum(t => t.Price) * 0.87m;

            context.SaveChanges();

            return Redirect($"/Albums/Details?id={album.Id}");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/");
            }

            var albumId = httpRequest.QueryData["albumId"].ToString();
            var trackId = httpRequest.QueryData["trackId"].ToString();

            using var context = new RunesDbContext();

            var track = context.Tracks.Find(trackId);

            ViewData.Add("AlbumId", albumId);
            ViewData.Add("Link", WebUtility.UrlDecode(track.Link));
            ViewData.Add("Name", WebUtility.UrlDecode(track.Name));
            ViewData.Add("Price", $"${track.Price:f2}");

            return View();
        }
    }
}
