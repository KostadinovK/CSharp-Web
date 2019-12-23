using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Extensions;
using SIS.MvcFramework.Result;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        protected Dictionary<string, object> ViewData { get; set; } = new Dictionary<string, object>();

        protected Controller()
        {
           
        }

        protected bool IsLoggedIn(IHttpRequest httpRequest)
        {
            return httpRequest.Session.ContainsParameter("username");
        }

        protected void SignIn(IHttpRequest httpRequest, string id, string username, string email)
        {
            httpRequest.Session.AddParameter("username", username);
            httpRequest.Session.AddParameter("id", id);
            httpRequest.Session.AddParameter("email", email);
        }

        protected void SignOut(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();
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
