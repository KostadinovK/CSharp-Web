using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;

namespace SIS.MvcFramework.Result
{
    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(string message, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.NotFound) : base(httpResponseStatusCode)
        {
            Content = Encoding.UTF8.GetBytes(message);
        }
    }
}
