using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.MvcFramework.Extensions;
using SIS.MvcFramework.Identity;
using SIS.MvcFramework.Result;
using SIS.MvcFramework.ViewEngine;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {

        private IViewEngine viewEngine = new SisViewEngine();
        protected Dictionary<string, object> ViewData { get; set; } = new Dictionary<string, object>();

        public IHttpRequest Request { get; set; }

        public Principal User => 
            Request.Session.ContainsParameter("principal")
            ? (Principal) Request.Session.GetParameter("principal")
            : null;

        protected Controller()
        {
           
        }

        protected bool IsLoggedIn()
        {
            return Request.Session.ContainsParameter("principal");
        }

        protected void SignIn(string id, string username, string email)
        {
            Request.Session.AddParameter("principal", new Principal()
            {
                Id = id,
                Username = username,
                Email = email
            });
        }

        protected void SignOut()
        {
            Request.Session.ClearParameters();
        }

        protected ActionResult View([CallerMemberName] string view = null)
        {
            return this.View<object>(null, view);
        }

        protected ActionResult View<T>(T model = null, [CallerMemberName] string view = null)
            where T : class
        {
            var controllerName = this.GetType().Name.Replace("Controller", "");
            var viewName = view;
            var viewContent = System.IO.File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");
            viewContent = viewEngine.GetHtml(viewContent, model);

            var layoutContent = System.IO.File.ReadAllText("Views/_Layout.html");
            layoutContent = viewEngine.GetHtml(layoutContent, model);

            layoutContent = layoutContent.Replace("@RenderBody()", viewContent);

            return new HtmlResult(layoutContent);
        }

        protected ActionResult Redirect(string location)
        {
            return new RedirectResult(location);
        }

        protected ActionResult Xml(object obj)
        {
            return new XmlResult(obj.ToXml());
        }

        protected ActionResult Json(object obj)
        {
            return new JsonResult(obj.ToJson());
        }

        protected ActionResult File(byte[] fileContent)
        {
            return new FileResult(fileContent);
        }

        protected ActionResult NotFound(string message = "")
        {
            return new NotFoundResult(message); 
        }
    }
}
