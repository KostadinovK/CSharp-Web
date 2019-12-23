using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Http;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Identity;
using SIS.MvcFramework.Result;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet(Url = "/")]
        public ActionResult IndexSlash()
        {
            return Index();
        }

        [Authorize]
        public ActionResult Index()
        {
            if (IsLoggedIn())
            {
                ViewData.Add("Username",((Principal) Request.Session.GetParameter("principal")).Username);
                return this.View("/Index-Logged");
            }

            return this.View();
        }
    }
}
