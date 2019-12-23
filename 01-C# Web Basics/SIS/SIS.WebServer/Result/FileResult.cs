using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;

namespace SIS.MvcFramework.Result
{
    public class FileResult : ActionResult
    {
        public FileResult(byte[] fileContent, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) : base(httpResponseStatusCode)
        {
            Headers.AddHeader(new HttpHeader(HttpHeader.ContentLength, fileContent.Length.ToString()));
            Headers.AddHeader(new HttpHeader(HttpHeader.ContentDisposition, "attachment"));
            Content = fileContent;
        }
    }
}
