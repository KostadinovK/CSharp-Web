using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace SIS.Demo
{
    public class HomeController : BaseController
    {
        public IHttpResponse Home(IHttpRequest request)
        {
            return this.View();
        }
    }
}
