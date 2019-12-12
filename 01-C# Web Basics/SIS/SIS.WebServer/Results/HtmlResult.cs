using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;

namespace SIS.WebServer.Results
{
    public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseStatusCode statusCode, string contentType = "text/html; charset=utf-8")
            : base(statusCode)
        {
            Headers.AddHeader(new HttpHeader("Content-Type", contentType));
            Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
