using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using IRunes.Data;
using IRunes.Models.Models;
using IRunes.Services;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Http;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;

namespace IRunes.App.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITrackService trackService;
        private readonly IAlbumService albumService;

        public TracksController()
        {
            trackService = new TrackService();
            albumService = new AlbumService();
        }

        [Authorize]
        public ActionResult Create()
        {
            var albumId = Request.QueryData["albumId"];
            ViewData.Add("AlbumId", albumId);

            return View();
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm()
        {
            var albumId = Request.QueryData["albumId"].ToString();
            var name = ((ISet<string>) Request.FormData["name"]).FirstOrDefault();
            var link = ((ISet<string>)Request.FormData["link"]).FirstOrDefault();
            var price = ((ISet<string>)Request.FormData["price"]).FirstOrDefault();

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

            if (!albumService.AddTrackToAlbum(albumId, track))
            {
                return Redirect("/Albums/All");
            }

            return Redirect($"/Albums/Details?id={albumId}");
        }

        [Authorize]
        public ActionResult Details()
        {
            var albumId = Request.QueryData["albumId"].ToString();
            var trackId = Request.QueryData["trackId"].ToString();

            var track = trackService.GetTrackById(trackId);

            ViewData.Add("AlbumId", albumId);
            ViewData.Add("Link", WebUtility.UrlDecode(track.Link));
            ViewData.Add("Name", WebUtility.UrlDecode(track.Name));
            ViewData.Add("Price", $"${track.Price:f2}");

            return View();
        }

        protected bool IsValid(object obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);


            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
