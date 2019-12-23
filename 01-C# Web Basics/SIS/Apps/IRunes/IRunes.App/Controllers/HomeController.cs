using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Http;
using SIS.MvcFramework.Result;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet(Url = "/")]
        public ActionResult IndexSlash(IHttpRequest httpRequest)
        {
            return Index(httpRequest);
        }

        public ActionResult Index(IHttpRequest httpRequest)
        {
            if (IsLoggedIn(httpRequest))
            {
                ViewData.Add("Username", httpRequest.Session.GetParameter("username").ToString());
                return this.View("/Index-Logged");
            }

            return this.View();
        }
    }
}
