using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Sessions.Contracts;

namespace SIS.HTTP.Requests.Contracts
{
    public interface IHttpRequest
    {
        string Url { get; }

        string Path { get; }

        Dictionary<string, object> FormData { get; }

        Dictionary<string, object> QueryData { get; }

        IHttpHeaderCollection Headers { get; }

        HttpRequestMethod RequestMethod { get; }

        IHttpCookieCollection Cookies { get; }

        IHttpSession Session { get; set; }
    }
}
