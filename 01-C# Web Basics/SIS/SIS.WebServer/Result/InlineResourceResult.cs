using SIS.HTTP.Enums;
using SIS.HTTP.Headers;

namespace SIS.MvcFramework.Result
{
    public class InlineResourceResult : ActionResult
    {
        public InlineResourceResult(byte[] content, HttpResponseStatusCode responseStatusCode) : base(responseStatusCode)
        {
            Headers.AddHeader(new HttpHeader(HttpHeader.ContentLength, content.Length.ToString()));
            Headers.AddHeader(new HttpHeader(HttpHeader.ContentDisposition, "inline"));
            Content = content;
        }
    }
}
