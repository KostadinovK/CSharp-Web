using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace SIS.Demo
{
    public class HomeController : BaseController
    {
        public IHttpResponse Home(IHttpRequest request)
        {
            HttpRequest = request;

            return this.View();
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            request.Session.AddParameter("username", "Pesho");
            return Redirect("/");
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.ClearParameters();
            return Redirect("/");
        }
    }
}
