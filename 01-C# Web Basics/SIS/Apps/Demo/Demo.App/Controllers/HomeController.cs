using SIS.HTTP.Requests;
using SIS.HTTP.Responses;

namespace SIS.Demo
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpRequest request) : base(request) { }

        public IHttpResponse Home()
        {
            if (IsLoggedIn())
            {
                ViewData.Add("HelloMessage", "Logged In");
            }
            else
            {
                ViewData.Add("HelloMessage", "Not logged In");
            }

            return this.View();
        }

    }
}
