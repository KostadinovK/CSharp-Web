using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using IRunes.Data;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
    public class AlbumsController : BaseController
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
                        sb.AppendLine($"<a href=/Albums/Details?id={album.Id}>{WebUtility.UrlDecode(album.Name)}</a>");
                    }
                }
            }

            sb.AppendLine("</div>");

            ViewData.Add("Albums", sb.ToString().TrimEnd());

            return View();
        }
    }
}
