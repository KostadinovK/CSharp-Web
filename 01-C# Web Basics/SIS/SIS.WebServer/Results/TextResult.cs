using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;

namespace SIS.WebServer.Results
{
    public class TextResult : HttpResponse
    {
        public TextResult(string content, HttpResponseStatusCode statusCode, string contentType = "text/plain; charset=utf-8")
            :base(statusCode)
        {
            Content = Encoding.UTF8.GetBytes(content);
            Headers.AddHeader(new HttpHeader("Content-Type", contentType));
        }

        public TextResult(byte[] content, HttpResponseStatusCode statusCode, string contentType = "text/plain; charset=utf-8")
            : base(statusCode)
        {
            Content = content;
            Headers.AddHeader(new HttpHeader("Content-Type", contentType));
        }
    }
}
