using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Common;

namespace SIS.HTTP.Cookies
{
    public class HttpCookie
    {
        private const int HttpCookieDefaultExpirationDays = 3;
        private const string HttpCookieDefaultPath = "/";

        public string Key { get; }

        public string Value { get; }

        public DateTime Expires { get; private set; }

        public string Path { get; set; }

        public bool IsNew { get; set; }

        public bool HttpOnly { get; set; }

        public HttpCookie(string key, string value, int expires = HttpCookieDefaultExpirationDays, string path = HttpCookieDefaultPath)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            Key = key;
            Value = value;
            Expires = DateTime.UtcNow.AddDays(expires);
            Path = path;
        }

        public HttpCookie(string key, string value, bool isNew, int expires = HttpCookieDefaultExpirationDays, string path = HttpCookieDefaultPath)
            : this(key, value, expires, path)
        {
            IsNew = isNew;
        }

        public void Delete()
        {
            Expires = DateTime.UtcNow.AddDays(-1);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{Key}={Value}; Expires={Expires:R}");

            if (HttpOnly)
            {
                sb.Append("; HttpOnly");
            }

            sb.Append($"; Path={Path}");

            return sb.ToString();
        }
    }
}
