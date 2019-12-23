using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;

namespace SIS.MvcFramework.Result
{
    public class XmlResult : ActionResult
    {
        public XmlResult(string xmlContent, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) : base(httpResponseStatusCode)
        {
            AddHeader(HttpHeader.ContentType, "application/xml");
            this.Content = Encoding.UTF8.GetBytes(xmlContent);
        }
    }
}
