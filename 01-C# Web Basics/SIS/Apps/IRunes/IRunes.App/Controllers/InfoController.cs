using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
{
    public class InfoController : Controller
    {
        public IHttpResponse About(IHttpRequest httpRequest)
        {
            return View();
        }
    }
}
