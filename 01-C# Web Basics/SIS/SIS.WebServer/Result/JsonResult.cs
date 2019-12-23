using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;

namespace SIS.MvcFramework.Result
{
    public class JsonResult : ActionResult
    {
        public JsonResult(string jsonContent, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) : base(httpResponseStatusCode)
        {
            AddHeader(HttpHeader.ContentType, "application/json");
            this.Content = Encoding.UTF8.GetBytes(jsonContent);
        }
    }
}
