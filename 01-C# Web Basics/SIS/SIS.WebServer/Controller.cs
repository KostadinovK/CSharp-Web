using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.MvcFramework.Extensions;
using SIS.MvcFramework.Identity;
using SIS.MvcFramework.Result;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
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

        private string ParseTemplate(string viewContent)
        {
            foreach (var param in ViewData)
            {
                viewContent = viewContent.Replace($"@Model.{param.Key}", param.Value.ToString());
            }

            return viewContent;
        }

        protected ActionResult View([CallerMemberName] string view = null)
        {
            var controllerName = this.GetType().Name.Replace("Controller", "");
            var viewName = view;
            var content = System.IO.File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            content = ParseTemplate(content);
            
            return new HtmlResult(content, HttpResponseStatusCode.Ok);
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
