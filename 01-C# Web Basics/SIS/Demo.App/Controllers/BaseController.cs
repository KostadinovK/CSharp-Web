using System.IO;
using System.Runtime.CompilerServices;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;

namespace SIS.Demo
{
    public abstract class BaseController
    {
        public IHttpResponse View([CallerMemberName] string view = null)
        {
            var controllerName = GetType().Name.Replace("Controller", string.Empty);
            var viewName = view;

            var viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            var httpResponse = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
           
            return httpResponse;
        }
    }
}
