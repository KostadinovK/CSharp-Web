using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponseStatusCode StatusCode { get; set; }
        public IHttpHeaderCollection Headers { get; }
        public IHttpCookieCollection Cookies { get; }

        public byte[] Content { get; set; }

        public HttpResponse()
        {
            Headers = new HttpHeaderCollection();
            Content = new byte[0];
            Cookies = new HttpCookieCollection();
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));

            StatusCode = statusCode;
        }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));

            Headers.AddHeader(header);
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            Cookies.AddCookie(cookie);
        }

        public byte[] GetBytes()
        {
            var responseHeaderBytes = Encoding.UTF8.GetBytes(this.ToString());

            var responseBytes = new byte[responseHeaderBytes.Length + Content.Length];

            for (int i = 0; i < responseHeaderBytes.Length; i++)
            {
                responseBytes[i] = responseHeaderBytes[i];
            }

            for (int i = 0; i < Content.Length; i++)
            {
                responseBytes[responseHeaderBytes.Length + i] = Content[i];
            }

            return responseBytes;
        }

        public override string ToString()
        { 
            var sb = new StringBuilder();

            sb.Append($"{GlobalConstants.HttpOneProtocolFragment} {(int) StatusCode} {StatusCode.ToString()}");
            sb.Append(GlobalConstants.HttpNewLine);
            sb.Append(Headers);
            sb.Append(GlobalConstants.HttpNewLine);
            if (Cookies.HasCookies())
            {
                foreach (var cookie in Cookies)
                {
                    sb.Append($"Set-Cookie: {cookie}");
                    sb.Append(GlobalConstants.HttpNewLine);
                }
            }
            sb.Append(GlobalConstants.HttpNewLine);

            return sb.ToString();
        }
    }
}
