using System.IO;
using System.Runtime.CompilerServices;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;

namespace SIS.Demo
{
    public abstract class BaseController
    {

        protected IHttpRequest HttpRequest { get; set; }

        private bool IsLoggedIn()
        {
            return HttpRequest.Session.ContainsParameter("username");
        }

        private string ParseTemplate(string viewContent)
        {
            if (IsLoggedIn())
            {
                return viewContent.Replace("@Model.HelloMessage", $"Hello, {HttpRequest.Session.GetParameter("username")}!");
            }

            return viewContent.Replace("@Model.HelloMessage", "Hello from the server!");
        }

        public IHttpResponse View([CallerMemberName] string view = null)
        {
            var controllerName = GetType().Name.Replace("Controller", string.Empty);
            var viewName = view;

            var viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = ParseTemplate(viewContent);

            var httpResponse = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
           
            return httpResponse;
        }

        public IHttpResponse Redirect(string location)
        {
            return new RedirectResult(location);
        }
    }
}
