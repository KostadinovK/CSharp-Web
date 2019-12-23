using System.Collections.Generic;
using System.Reflection;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Result;

namespace IRunes.App.Controllers
{
    public class InfoController : Controller
    {
        public ActionResult About(IHttpRequest httpRequest)
        {
            return View();
        }

        public ActionResult File(IHttpRequest request)
        {
            string folderPrefix = "/../../../../";
            string resourceFolder = "Resources/";
            string assemblyFolderPath = this.GetType().Assembly.Location;

            string fullPath = assemblyFolderPath + folderPrefix + resourceFolder + request.QueryData["file"];
            if (System.IO.File.Exists(fullPath))
            {
                string mimeType = null;
                string fileName = null;

                byte[] content = System.IO.File.ReadAllBytes(fullPath);
                return File(content);
            }

            return NotFound();
        }
    }
}
