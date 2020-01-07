using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;

namespace Panda.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Redirect("/Home/Index");
        }

        public IActionResult Index()
        {
            if (User != null)
            {
                return this.View("IndexLoggedIn");
            }

            return this.View();
        }
    }
}
